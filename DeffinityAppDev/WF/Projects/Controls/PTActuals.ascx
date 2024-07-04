<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_PTActuals"  Codebehind="PTActuals.ascx.cs" %>
<%@ Register Src="~/WF/Projects/Controls/Matrieals.ascx" TagName="Material" TagPrefix="UcMaterial" %>
<%@ Register Src="~/WF/Projects/Controls/PTLabourTracker.ascx" TagName="Labour" TagPrefix="uc3" %>
<%@ Register Src="~/WF/Projects/Controls/PTMaterialTracker.ascx" TagName="Material" TagPrefix="uc3" %>
<%@ Register Src="~/WF/Projects/Controls/PTMiscTracker.ascx" TagName="Misc" TagPrefix="uc7" %>
<%@ Register Src="~/WF/Projects/Controls/PTPMHours.ascx" TagName="PMHours" TagPrefix="uc4" %>
<%@ Register Src="~/WF/Projects/Controls/PTExpense.ascx" TagName="PTExpense" TagPrefix="uc5" %>
<%@ Register Src="~/WF/Projects/Controls/PTStaffHours.ascx" TagName="StaffHours" TagPrefix="uc6" %>

<%@ Register Src="~/WF/Projects/controls/CreditGridCntl.ascx" TagName="CreditNote" TagPrefix="uc8" %>
<html>
<head>
     <script type="text/javascript">
           $(document).ready(function () {
               //$("#panelupload").hide();
               //$("#BtnImport").click(function (evt) {
               //    $("#panelupload").toggle();
               //    return false;
               //});
             
               function getParameterByName(name) {
                   name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
                   var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                       results = regex.exec(location.search);
                   return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
               }
               //$("#btndownload").click(function (evt) {
               //    debugger;
               //    var ptitle = getParameterByName("project");
               //    debugger;
               //    if (ptitle != 0) {
               //        debugger;
               //        var options = {};
               //        options.url = "DownloadHandler.ashx?rid=" + ptitle;
               //        debugger;
               //        options.type = "POST";
               //       // options.data = data;
               //        options.contentType = false;
               //        options.processData = false;
               //        options.success = function (result) {
               //            $("#SuccessMessage").html(result);
               //            // location.reload(true);
               //        };
               //        options.error = function (err) { $("#ErrorMessage").html(err.statusText); };
               //        $.ajax(options);
               //    }
               //    return false;
               //});
             
           });
       </script>
</head>
<body>
<asp:Label ID="lblError1" runat="server" Visible="false" ForeColor="Red" Text="<%$ Resources:DeffinityRes,Label%>"></asp:Label>
<div class="form-group">
    
                <asp:Label ID="lblacual" ForeColor="red" runat="server"></asp:Label>
                <asp:ValidationSummary ID="getActual" runat="server" ValidationGroup="Footer_Expenses" />
                <asp:ValidationSummary ID="Getexpenses" runat="server" ValidationGroup="External_Expenses" />
                <asp:ValidationSummary ID="Getexpensestext" runat="server" ValidationGroup="ExternalExpenses1" />
                <asp:ValidationSummary ID="GetExpensesType" runat="server" ValidationGroup="GetExpensesType" />
                <asp:ValidationSummary ID="ValidationSummary5" runat="server" ValidationGroup="Group10" />
                <asp:ValidationSummary ID="ValidationSummary6" runat="server" ValidationGroup="Group100" />
                <asp:ValidationSummary ID="GETExpenses12" runat="server" ValidationGroup="GETExpenses" />
                <asp:ValidationSummary ID="ValidationSummary7" runat="server" ValidationGroup="Group2" />
                <asp:ValidationSummary ID="GETttt" runat="server" ValidationGroup="ExterNal_ExpensesFooterInert" />
                <asp:ValidationSummary ID="ValidationOnTimesheet" runat="server" ValidationGroup="ValidateResource" />
                <asp:ValidationSummary ID="ValidationSummary8" runat="server" ValidationGroup="GroupInsert" />
            
</div>
    <div class="form-group well">
          <div class="col-md-12">
<div class="form-group">
        <div class="col-md-12 text-bold">
        <strong>   <%= Resources.DeffinityRes.Summary%> </strong>
            <hr class="no-top-margin" />
            </div>
    </div>

   
<div class="form-group">
      <div class="col-md-6">
          <div class="row">
              <label class="col-sm-8 control-label">Total labour cost:</label>
                                      <div class="col-sm-4"><asp:Label ID="lblTotalTimeBuying" runat="server"></asp:Label>
					</div>
          </div>
            <div class="row">
                 <label class="col-sm-8 control-label">Total material/product cost price:</label>
                                      <div class="col-sm-4"> <asp:Label ID="lblTotalMaterialCost" runat="server"></asp:Label>
					</div>
          </div>
            <div class="row">
                 <label class="col-sm-8 control-label">Total cost of expenses:</label>
                                      <div class="col-sm-4"><asp:Label ID="lblExpenses_BuyingCost" runat="server"></asp:Label>
					</div>
          </div>
            <div class="row">
                 <label class="col-sm-8 control-label">Total external costs:</label>
                                      <div class="col-sm-4"> <asp:Label ID="lblExternalExpenses" runat="server"></asp:Label>
					</div>
          </div>
             <div class="row">
                 <label class="col-sm-8 control-label">Total project costs:</label>
                                      <div class="col-sm-4"> <asp:Label ID="lblTotalprojectBuyingCost" runat="server" Font-Bold="true"></asp:Label>
					</div>
          </div>
            <div class="row">
                 <label class="col-sm-8 control-label">Credit note :</label>
                                      <div class="col-sm-4"> <asp:Label ID="lblCreditnote" runat="server" Font-Bold="true" Text="0"></asp:Label>
					</div>
          </div>
	</div>
	<div class="col-md-6">
           <div class="row">
                <label class="col-sm-8 control-label">Total time selling price:</label>
                                      <div class="col-sm-4"> <asp:Label ID="tbltotaltimeselling" runat="server"></asp:Label>
					</div>
          </div>
            <div class="row">
                 <label class="col-sm-8 control-label">Total material/product sell price:</label>
                                      <div class="col-sm-4"> <asp:Label ID="lblTotalMaterialSell" runat="server"></asp:Label>
					</div>
          </div>
            <div class="row">
                 <label class="col-sm-8 control-label">Total expenses selling:</label>
                                      <div class="col-sm-4"><asp:Label ID="lblExpenses_SellingCost" runat="server"></asp:Label>
					</div>
          </div>
           
            <div class="row">
                 <label class="col-sm-8 control-label">Total selling price:</label>
                                      <div class="col-sm-4"> <asp:Label ID="lbltotalsellingpriceofproject" runat="server" Font-Bold="true"></asp:Label>
					</div>
          </div>
            <div class="row">

          </div>
	</div>
	
</div>


<div>
    <label style="font-weight: bold">
        <%= Resources.DeffinityRes.ProjectFee%>
        <%--Selling Price--%>
        :</label>
    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblProjectbudget" runat="server" Font-Bold="true"
        Style="text-align: right">
    </asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <label style="font-weight: bold">
        <%= Resources.DeffinityRes.Actualcoststodate%>
        :</label>
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="lblprojectactualtotal" runat="server" Font-Bold="true" Style="text-align: right"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <label style="font-weight: bold">
        Total Hours:</label>&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="lblTotalHours" runat="server" Font-Bold="true" Style="text-align: right"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <label style="font-weight: bold">
          Live Profitability :   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblLiveprofitability" runat="server" Text="0" Font-Bold="true" Style="text-align: right"></asp:Label>
      </label>
</div>
<div class="clr">
</div>
<br />
<br />
<div style="height: 20px;">
    <label style="font-weight: bold">
        Internal Overhead % :</label>
    <asp:Label ID="lblInternalOverheadPer" runat="server" Font-Bold="true" Style="text-align: right" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <label style="font-weight: bold">
        Overhead Value :</label>
    <asp:Label ID="lblOverheadVal" runat="server" Font-Bold="true" Style="text-align: right" />
    &nbsp;&nbsp;
</div>
</div>
        </div>
    <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong>  Timesheet Entries</strong>
            <hr class="no-top-margin" />
            </div>
    </div>
<div class="form-group form-inline">
     <div class="col-md-12">
            <div class="col-md-3">
                  <label class="col-sm-5 control-label">  <%= Resources.DeffinityRes.Resource%></label>
                                      <div class="col-sm-7"> <asp:DropDownList ID="ddlcustomer_Timesheet" runat="server" DataSourceID="DDLCustomer"
                    DataTextField="ContractorName" DataValueField="ContractorID" SkinID="ddl_80">
                </asp:DropDownList>
					</div>
                </div>
          <div class="col-md-3">
                <label class="col-sm-5 control-label form-inline"> <%= Resources.DeffinityRes.StartDate%></label>
                                      <div class="col-sm-7 form-inline"> <asp:TextBox ID="txt_StartDate" runat="server" SkinID="Date" MaxLength="10"></asp:TextBox>
                <asp:Label ID="imgStartDate" runat="server" SkinID="Calender" ToolTip="<%$ Resources:DeffinityRes,Pickadate%>" />
					</div>
                </div>
          <div class="col-md-3">
                <label class="col-sm-5 control-label form-inline">   <%= Resources.DeffinityRes.EndDate%></label>
                                      <div class="col-sm-7 form-inline"> <asp:TextBox ID="txt_EndDate" runat="server" SkinID="Date" MaxLength="10"></asp:TextBox><asp:Label
                    ID="imgEndDate" runat="server" SkinID="Calender" ToolTip="<%$ Resources:DeffinityRes,Pickadate%>"/>
					</div>
                </div>
          <div class="col-md-3 form-inline">
             
               <asp:Button ID="btn_ViewTimesheet" runat="server" SkinID="btnDefault" Text="View"
                    OnClick="btn_ViewTimesheet_Click" ValidationGroup="ValidateResource" />
                <a href="../../WF/Reports/Timeandexpenses.aspx?Project=<%=getProject%>" target="_blank" style="font-weight: bold;">
                    <%= Resources.DeffinityRes.TimeandExpensesReport%></a>
                <asp:LinkButton ID="lnkReport" runat="server" SkinID="BtnLink" Font-Bold="true"
                    Text="<%$ Resources:DeffinityRes,TimeandExpensesReport%>" OnClick="lnkReport_Click"
                    Visible="false"></asp:LinkButton>
                </div>
         </div>
    
</div>
    <div class="form-group form-inline">
         <div class="col-md-8 form-inline">
             </div>
     <div class="col-md-4 form-inline pull-right" style="vertical-align:bottom;float:right;">
          <asp:HyperLink ID="btndownload" runat="server"
                     ClientIDMode="Static" Text="Download Template" SkinID="Button" Style="vertical-align:bottom;float:right"></asp:HyperLink>
                                    <asp:Button ID="BtnImport" runat="server" Text="Upload Timesheet" ClientIDMode="Static" Style="float:right" />
         </div>
        </div>
    <div class="form-group" visible="false">
        <ajaxToolkit:CalendarExtender ID="CalendarExtender600"  runat="server"
                    PopupButtonID="imgStartDate" TargetControlID="txt_StartDate" CssClass="MyCalendar">
                </ajaxToolkit:CalendarExtender>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender190"  runat="server"
                    PopupButtonID="imgEndDate" TargetControlID="txt_EndDate" CssClass="MyCalendar">
                </ajaxToolkit:CalendarExtender>
                <asp:RegularExpressionValidator ID="RegularExpresghgfhgf6" runat="server" ControlToValidate="txt_EndDate"
                    Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Entervaliddateinenddatefield%>"
                    ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                    ValidationGroup="ValidateResource"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RGt1" runat="server" ControlToValidate="txt_StartDate"
                    Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Entervaliddateinstartdatefield%>"
                    ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                    ValidationGroup="ValidateResource"></asp:RegularExpressionValidator>
                <asp:CompareValidator ID="c1" runat="server" ControlToCompare="txt_StartDate" ControlToValidate="txt_EndDate"
                    Display="none" Type="Date" Operator="GreaterThanEqual" ErrorMessage="<%$ Resources:DeffinityRes,startdatecantgrthenenddate%>"
                    ValidationGroup="ValidateResource"></asp:CompareValidator>
         <asp:SqlDataSource ID="DDLCustomer" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                    SelectCommand="DN_ContractorTimeSheet" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="Project" QueryStringField="project" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
        </div>

<div>
    <div>
        <asp:Label ID="lblActualMsg" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
    </div>
    <asp:Panel Width="100%" runat="server" ID="Panel10" >
           <ajaxtoolkit:modalpopupextender id="mdlUpload" cancelcontrolid="lblUploadCancel"
                        runat="server" backgroundcssclass="modalBackground" targetcontrolid="BtnImport"
                        popupcontrolid="pnlUpload">
                    </ajaxtoolkit:modalpopupextender>
    <asp:Panel ID="pnlUpload" runat="server" BackColor="White" Style="display: none"
                        Width="600px" Height="300px" BorderStyle="Double" BorderColor="LightSteelBlue" ScrollBars="None">
        <div class="row">
          <div class="col-md-10">
 <strong> <%= Resources.DeffinityRes.Uploadfile%> </strong> 
<hr class="no-top-margin" />
	</div>
             <div class="col-md-2 pull-right">
                   <asp:LinkButton ID="btnClosePop" runat="server" SkinID="BtnLinkClose" OnClick="btnClosePop_Click" />
                 </div>
</div>
                       
        <div class="col-md-12">
             <asp:Label ID="lblUploadCancel" runat="server"></asp:Label>
            </div>
         <div id="panelupload" runat="server" >
                     <iframe id="frm_setpage" name="frm_setpage" runat="server" width="570" height="250"
                          frameborder="0" scrolling="no" visible="true"  ></iframe>
          </div>
        </asp:Panel>
        <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" Width="100%"
            EmptyDataText="<%$ Resources:DeffinityRes,NoTimesheetsDataFound%>" ShowFooter="True"
            EnableViewState="true" OnRowCancelingEdit="GridView4_RowCancelingEdit" OnRowCommand="GridView4_RowCommand"
            OnRowEditing="GridView4_RowEditing" OnRowUpdating="GridView4_RowUpdating" OnRowDataBound="GridView4_RowDataBound"
            AllowPaging="True" OnPageIndexChanging="GridView4_PageIndexChanging" PageSize="50">
            <Columns>
                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Edit%>" HeaderStyle-CssClass="header_bg_l">
                    <HeaderStyle Width="50px" />
                    <ItemStyle Width="50px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonEdit" runat="server" Enabled="<%#CommandField()%>"
                            CausesValidation="false" CommandName="Edit" CommandArgument='<%# Bind("ID")%>'
                            SkinID="BtnLinkEdit" ToolTip="<%$ Resources:DeffinityRes,Edit%>"></asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="<%$ Resources:DeffinityRes,Update%>"
                            CommandArgument='<%# Bind("ID")%>' ValidationGroup="Group2" SkinID="BtnLinkUpdate"
                            ToolTip="<%$ Resources:DeffinityRes,Update%>"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                            SkinID="BtnLinkCancel" ToolTip="<%$ Resources:DeffinityRes,Cancel%>"></asp:LinkButton>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:LinkButton ID="LinkButtonInsert" runat="server" Enabled="<%#CommandField()%>"
                            CommandName="Insert" Text="<%$ Resources:DeffinityRes,Insert%>" ValidationGroup="GroupInsert"
                            SkinID="BtnLinkAdd" ToolTip="<%$ Resources:DeffinityRes,Insert%>"></asp:LinkButton>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblentryid" runat="server" Visible="false" Text='<%# Eval("ID")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblcontratorID" runat="server" Visible="false" Text='<%# Eval("ContractorsID")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ID" HeaderText="<%$ Resources:DeffinityRes,ID%>" Visible="False" />
                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Date%>">
                    <FooterStyle Width="110px" CssClass="form-inline" />
                    <ItemStyle Width="110px" CssClass="form-inline" />
                    <HeaderStyle Width="110px" />
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEndDate" runat="server" Text='<%# Bind("DateEntered","{0:d}") %>'
                            SkinID="Date"></asp:TextBox>
                        <asp:Label ID="imgbtnenddate6" runat="server" SkinID="Calender" ToolTip="<%$ Resources:DeffinityRes,Pickadate%>"
                            />
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender6"  runat="server"
                            PopupButtonID="imgbtnenddate6" TargetControlID="txtEndDate" CssClass="MyCalendar">
                        </ajaxToolkit:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEndDate"
                            Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Datecantbeblank%>" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtEndDate"
                            Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Entervaliddtindtfld%>"
                            ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                            ValidationGroup="Group2">*</asp:RegularExpressionValidator>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtDate_footer" runat="server" SkinID="Date"></asp:TextBox>
                        <asp:Label ID="imgFooterDate" runat="server" SkinID="Calender" ToolTip="<%$ Resources:DeffinityRes,Pickadate%>"
                            />
                        <ajaxToolkit:CalendarExtender ID="CalenderExtender_footer"  runat="server"
                            PopupButtonID="imgFooterDate" TargetControlID="txtDate_footer" CssClass="MyCalendar">
                        </ajaxToolkit:CalendarExtender>
                        <asp:RequiredFieldValidator ID="Rfv_footer" runat="server" ControlToValidate="txtDate_footer"
                            Display="None" ErrorMessage="<%$ Resources:DeffinityRes,PlsselectDate%>" ValidationGroup="GroupInsert"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtDate_footer"
                            Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Entervaliddtindtfld%>"
                            ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                            ValidationGroup="GroupInsert"></asp:RegularExpressionValidator>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblEndDate" runat="server" Text='<%# Bind("DateEntered","{0:d}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ResourceName%>">
                    <ItemStyle Width="110px" />
                    <HeaderStyle Width="110px" />
                    <ItemTemplate>
                        <asp:Label ID="lblProject" runat="server" Text='<%# Bind("ContractorName") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddlContractor_footer" runat="server" SkinID="ddl_125px" DataSourceID="SqlDataSource123"
                            DataTextField="ContractorName" DataValueField="ID" OnSelectedIndexChanged="ddlContractor_footer_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="Rfv_contractor" runat="server" ControlToValidate="ddlContractor_footer"
                            InitialValue="0" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectresource%>"
                            ValidationGroup="GroupInsert" Display="None"></asp:RequiredFieldValidator>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Project%>" ItemStyle-CssClass="col-nowrap" FooterStyle-CssClass="col-nowrap"  ItemStyle-Width="200px" FooterStyle-Width="200px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblProjectTitle" runat="server" Text='<%# Eval("ProjectTitle") %>'
                            Width="150px"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="150px" />
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlProjects" SkinID="ddl_125px" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlProjects_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:Label ID="lblRid" runat="server" Visible="false" Text='<%# Eval("ContractorsID")%>'></asp:Label>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblProjectTitle_footer" runat="server" Width="150px"></asp:Label><br/>
                        <asp:Label ID="TotalPageHours" runat="server" Text="Hours for this page" Font-Bold="true" />
                    </FooterTemplate>
                    <ItemStyle Width="125px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ProjectTask%>" ItemStyle-CssClass="col-nowrap" FooterStyle-CssClass="col-nowrap"  ItemStyle-Width="200px" FooterStyle-Width="200px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblProjectTasks" runat="server" Text='<%# Eval("ProjectTask") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="125px" />
                    <EditItemTemplate>
                        <asp:DropDownList ID="GetProjectTasks" SkinID="ddl_125px" runat="server">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="DdlProjectTasks_footer" SkinID="ddl_125px" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="Rfv_task" runat="server" ControlToValidate="DdlProjectTasks_footer"
                            InitialValue="0" ErrorMessage="<%$ Resources:DeffinityRes,PlsselectTask%>" ValidationGroup="GroupInsert"
                            Display="None"></asp:RequiredFieldValidator><br/>
                        <asp:Label ID="lblTothrspage" runat="server" Font-Bold="true"></asp:Label>
                    </FooterTemplate>
                    <ItemStyle Width="125px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,EntryType%>">
                    <HeaderStyle Width="100px" />
                    <ItemStyle Width="100px" />
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlEntry" SkinID="ddl_100px" runat="server" DataSourceID="SqlDataSourceEntry2"
                            DataTextField="EntryType" DataValueField="EntryTypeID" SelectedValue='<%# Bind("EntryTypeID") %>'>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblEntry" runat="server" Text='<%# Bind("EntryType") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddlEntry_footer" SkinID="ddl_100px" runat="server" DataSourceID="SqlDataSourceEntry2"
                            DataTextField="EntryType" DataValueField="EntryTypeID">
                        </asp:DropDownList>
                        <br />
                        <asp:Label ID="DispayTotal" runat="server" Text="<%$ Resources:DeffinityRes,Total%> "
                            Font-Bold="true" />
                    </FooterTemplate>
                    <FooterStyle Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Hours%>">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                    <EditItemTemplate>
                        <asp:TextBox ID="txtHours" runat="server" Text='<%# ChangeHoues(Eval("Hours").ToString())%>'
                            SkinID="Time" MaxLength="5"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="regex1" runat="server" ControlToValidate="txtHours"
                            ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$" ValidationGroup="Group2"
                            Display="None" Text="*" ErrorMessage="<%$ Resources:DeffinityRes,PleaseentervalidTime%> "></asp:RegularExpressionValidator>
                        <%--<asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtHours"
                                Display="None" ErrorMessage="Hours cannot be more than 24 " MaximumValue="24"
                                MinimumValue="0" Type="Double" ValidationGroup="Group2"></asp:RangeValidator>--%>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblhours" runat="server" Text='<%# ChangeHoues(Eval("Hours").ToString())%>'></asp:Label>
                    </ItemTemplate>
                    <FooterStyle HorizontalAlign="Right" />
                    <FooterTemplate>
                        <asp:TextBox ID="txtHours_footer" runat="server" SkinID="Time" MaxLength="5"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="regex1" runat="server" ControlToValidate="txtHours_footer"
                            ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$" ValidationGroup="GroupInsert"
                            Display="None" Text="*" ErrorMessage="<%$ Resources:DeffinityRes,PleaseentervalidTime%>"></asp:RegularExpressionValidator>
                        <br />
                        <asp:Label ID="AmountTotal" runat="server" Font-Bold="true" />
                    </FooterTemplate>
                    <FooterStyle Width="40px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PO Number">
                    <HeaderStyle Width="100px" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <ItemTemplate>
                        <asp:Label ID="lblPONumber" runat="server" Text='<%# Bind("PONumber") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="lblPONumber1" runat="server" Text='<%# Bind("PONumber") %>' Visible="false"></asp:Label>
                        <asp:DropDownList ID="ddlPONumber" runat="server" SkinID="ddl_100px">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddlPONumber_footer" Visible="false" runat="server" Width="90px">
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Site%>">
                    <HeaderStyle Width="100px" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <ItemTemplate>
                        <asp:Label ID="lblsite" runat="server" Text='<%# Bind("sitename") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlsite1" runat="server" SkinID="ddl_100px" DataSourceID="SqlDataSiteEdit"
                            DataTextField="Site" DataValueField="SiteID">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddlsite_footer" runat="server" SkinID="ddl_100px" DataSourceID="SqlDataSiteEdit"
                            DataTextField="Site" DataValueField="SiteID">
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,TotalBuyingCost%>">
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                    <HeaderStyle Width="60px" />
                    <EditItemTemplate>
                        <asp:TextBox ID="txttotalcost" runat="server" Text='<%# Bind("TotalCost","{0:#.00}") %>'
                            SkinID="Time"></asp:TextBox>
                        <asp:CompareValidator ID="HINI" runat="server" ControlToValidate="txttotalcost" Display="None"
                            ErrorMessage="<%$ Resources:DeffinityRes,Plsentervalidprice%>" Operator="DataTypeCheck"
                            Type="Double" ValidationGroup="Group2"></asp:CompareValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbltotalcost" runat="server" Text='<%# Bind("TotalCost","{0:N2}") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterStyle HorizontalAlign="Right" Width="40px" />
                    <FooterTemplate>
                        <asp:TextBox ID="txttotalcost_footer" runat="server" SkinID="Time"></asp:TextBox>
                        <asp:CompareValidator ID="Cv_cost_footer" runat="server" ControlToValidate="txttotalcost_footer"
                            Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Plsentervalidprice%>"
                            Operator="DataTypeCheck" Type="Double" ValidationGroup="GroupInsert"></asp:CompareValidator>
                        <br />
                        <asp:Label ID="lbltotalcost_BuyingCost" Font-Bold="true" runat="server" Text='<%# Bind("TotalCost","{0:N2}") %>'></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,TotalSellingPrice%>">
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                    <HeaderStyle Width="60px" />
                    <EditItemTemplate>
                        <asp:TextBox ID="txttotalSellingCost" runat="server" Text='<%# Bind("Total_SellingCost","{0:#.00}") %>'
                            SkinID="Time"></asp:TextBox>
                        <asp:CompareValidator ID="HINI6" runat="server" ControlToValidate="txttotalSellingCost"
                            Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Plsentervalidprice%>"
                            Operator="DataTypeCheck" Type="Double" ValidationGroup="Group2"></asp:CompareValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbltotal_SellingCost" runat="server" Text='<%# Bind("Total_SellingCost","{0:N2}") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterStyle HorizontalAlign="Right" Width="60px" />
                    <FooterTemplate>
                        <asp:TextBox ID="txttotalSellingCost_footer" runat="server" SkinID="Time"></asp:TextBox>
                        <asp:CompareValidator ID="CV_sellingcost" runat="server" ControlToValidate="txttotalSellingCost_footer"
                            Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Plsentervalidprice%>"
                            Operator="DataTypeCheck" Type="Double" ValidationGroup="GroupInsert"></asp:CompareValidator>
                        <br />
                        <asp:Label ID="lbltotalcost_SellingCost" Font-Bold="true" runat="server" Text="<%# Bind('Total_SellingCost','{0:N2}') %>"></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Notes%>">
                    <ItemStyle />
                    <EditItemTemplate>
                        <asp:TextBox ID="txtNotes" runat="server" SkinID="txt_125px" TextMode="MultiLine" Text='<%# Bind("Notes") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblnotes" runat="server" Text='<%# Bind("Notes") %>' ToolTip='<%#Bind("Notes") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtNotes_footer" runat="server" SkinID="txt_125px"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderStyle Width="30px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="ImageButton1" runat="server" Enabled="<%#CommandField()%>" CausesValidation="false"
                            CommandName="del" SkinID="BtnLinkDelete" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete the record?');" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource123" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
            SelectCommand="DN_Labourcontrators" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="0" Name="ProjectReference" QueryStringField="Project" />
                <asp:SessionParameter DefaultValue="0" Name="UID" DbType="Int32" SessionField="UID" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSourceEntry2" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
            SelectCommand="SELECT ID as EntryTypeID,EntryType FROM [TimesheetEntryType]">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSiteEdit" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
            SelectCommand="DN_TimesheetSite_Customer" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="0" Name="ProjectReference" QueryStringField="Project" />
            </SelectParameters>
        </asp:SqlDataSource>
        <%-- <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                                SelectCommand="select distinct PONumber,ID from customer_podatabase" SelectCommandType="Text">
                                                 <SelectParameters>
                                                 <asp:QueryStringParameter DefaultValue="0" Name="ProjectReference" QueryStringField="Project" />
                                                 </SelectParameters>
                          </asp:SqlDataSource>--%>
    </asp:Panel>
</div>

    <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong>  Bill of Labour </strong>
            <hr class="no-top-margin" />
            </div>
    </div>
<div>
    <uc3:Labour ID="Labour1" runat="server" />
</div>
<div class="clr">
</div>

    <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong>  Bill of Materials </strong>
            <hr class="no-top-margin" />
            </div>
    </div>
<div>
    <uc3:Material ID="Material1" runat="server" />
    <%--<ucmaterial:material id="ucMaterial" runat="server" />--%>
</div>


    <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong>  Bill of Miscellaneous </strong>
            <hr class="no-top-margin" />
            </div>
    </div>
<div>
    <uc7:Misc ID="Misc1" runat="server" />
    <%--<ucmaterial:material id="ucMaterial" runat="server" />--%>
</div>

    <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong>  PM Hours </strong>
            <hr class="no-top-margin" />
            </div>
    </div>
<div>
    <uc4:PMHours ID="PMHours1" runat="server" />
</div>

    <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong>  Staff Hours </strong>
            <hr class="no-top-margin" />
            </div>
    </div>
<div>
   <uc6:StaffHours ID="StaffHours1" runat="server" />
</div>
    <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong>  <%= Resources.DeffinityRes.Expenses%> </strong>
            <hr class="no-top-margin" />
            </div>
    </div>
    <div class="form-group">
          <div class="col-md-12">
              <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" Width="100%"
                    EmptyDataText="<%$ Resources:DeffinityRes,NoExpensesDataavailable%>" OnRowCancelingEdit="GridView5_RowCancelingEdit"
                    OnRowCommand="GridView5_RowCommand" OnRowEditing="GridView5_RowEditing" OnRowUpdating="GridView5_RowUpdating"
                    OnRowDataBound="GridView5_RowDataBound" OnRowDeleting="GridView5_RowDeleting">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderStyle Width="50px" HorizontalAlign="Center" />
                            <ItemStyle Width="50px" />
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkeditTandE" runat="server" Enabled="<%#CommandField()%>"
                                    CausesValidation="false" CommandName="Edit" CommandArgument='<%# Bind("ID")%>'
                                    SkinID="BtnLinkEdit" ToolTip="<%$ Resources:DeffinityRes,Edit%>">
                                </asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="LinkUpdateTandE" runat="server" CommandName="Update" Text="<%$ Resources:DeffinityRes,Update%>"
                                    CommandArgument='<%# Bind("ID")%>' ValidationGroup="Group100" SkinID="BtnLinkUpdate"
                                    ToolTip="<%$ Resources:DeffinityRes,Update%>"></asp:LinkButton>
                                <asp:LinkButton ID="LinkCancelTandE" runat="server" CausesValidation="false" CommandName="Cancel"
                                    SkinID="BtnLinkCancel" ToolTip="<%$ Resources:DeffinityRes,Cancel%>">
                                </asp:LinkButton>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:LinkButton CommandName="Insert_Footer" Enabled="<%#CommandField()%>" ID="expenses_btn"
                                    runat="server" SkinID="BtnLinkUpdate" ValidationGroup="Footer_Expenses" />
                                <asp:LinkButton ID="LinkCancelTandE" runat="server" CommandName="Cancel_ExterNalFooter"
                                    SkinID="BtnLinkCancel" ToolTip="<%$ Resources:DeffinityRes,Cancel%>">
                                </asp:LinkButton>
                            </FooterTemplate>
                            <FooterStyle Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblentryidTandE" runat="server" Visible="false" Text='<%# Eval("ID")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblcontratorIDexp" runat="server" Visible="false" Text='<%# Eval("ContractorsID")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Date%>">
                            <ItemStyle Width="120px" CssClass="form-inline" />
                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEndDateTandE" runat="server" Text='<%# Bind("TimeExpensesDate","{0:d}") %>'
                                    SkinID="Date"></asp:TextBox><asp:Label ID="imgbtnenddate9" runat="server" SkinID="Calender"/>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender6"  runat="server"
                                    PopupButtonID="imgbtnenddate9" TargetControlID="txtEndDateTandE" CssClass="MyCalendar">
                                </ajaxToolkit:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEndDateTandE"
                                    Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Datefieldempty%>" ValidationGroup="Group100"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtEndDateTandE"
                                    Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Entervaliddtindtfld%>"
                                    ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                    ValidationGroup="Group100">*</asp:RegularExpressionValidator>
                            </EditItemTemplate>
                            <FooterStyle Width="120px" CssClass="form-inline" />
                            <FooterTemplate>
                                <asp:RequiredFieldValidator ID="GetDate12" runat="server" ControlToValidate="txt_expenseDate"
                                    Display="None" ErrorMessage="<%$ Resources:DeffinityRes,PleaseenterDate%> " ValidationGroup="getActual"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_expenseDate"
                                    Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Entervaliddtindtfld%>"
                                    ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                    ValidationGroup="getActual"></asp:RegularExpressionValidator>
                                <asp:TextBox ID="txt_expenseDate" runat="server" ValidationGroup="getActual" Text='<%# Eval("TimeExpensesDate", "{0:d}") %>'
                                    MaxLength="10" SkinID="Date"></asp:TextBox>
                                <asp:Label ID="Image111" runat="server" SkinID="Calender" />
                                <ajaxToolkit:CalendarExtender CssClass="MyCalendar" ID="Clender121" runat="server"
                                    PopupButtonID="Image111" TargetControlID="txt_expenseDate" >
                                </ajaxToolkit:CalendarExtender>
                                <asp:RequiredFieldValidator ID="ReqTRE6" runat="server" ControlToValidate="txt_expenseDate"
                                    Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Datefieldempty%>" ValidationGroup="Footer_Expenses"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txt_expenseDate"
                                    Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Entervaliddtindtfld%>"
                                    ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                    ValidationGroup="Footer_Expenses"></asp:RegularExpressionValidator>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblEndDateTandE" runat="server" Text='<%# Bind("TimeExpensesDate","{0:d}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%-- <asp:TemplateField HeaderText="Associated to" Visible="false">
                                <HeaderStyle Width="110px" />
                                <ItemStyle Width="110" />
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlTitleTandE" runat="server" Width="200px" DataSourceID="TESqlProject_Footer"
                                        DataTextField="ProjectTitle" DataValueField="ProjectReference" SelectedValue='<%# Bind("ProjectReference") %>'>
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="TESqlProject_Footer" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                        SelectCommand="DN_TimeSheet_ProjectTile" SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:SessionParameter Name="ContractorID" SessionField="UID" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    <%-- <asp:RequiredFieldValidator ID="RequirVFT5" runat="server" ControlToValidate="ddlTitleTandE"
                                        ErrorMessage="Please select a project" InitialValue="0" Display="None"
                                        ValidationGroup="Group100"></asp:RequiredFieldValidator>  
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblProjectTanbE" runat="server" Width="200px" Text='<%# Bind("ProjectTitle") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlTitle_FooterExpenses" runat="server" Width="200px" DataSourceID="TESqlProjectExternal_Footer"
                                        DataTextField="ProjectTitle" DataValueField="ProjectReference" SelectedValue='<%# Bind("ProjectReference") %>'>
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="TESqlProjectExternal_Footer" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                        SelectCommand="DN_TimeSheet_ProjectTile" SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:SessionParameter Name="ContractorID" SessionField="UID" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    <%--    <asp:RequiredFieldValidator ID="RequirVFDTT5" runat="server" ControlToValidate="ddlTitle_FooterExpenses"
                                        ErrorMessage="Please select a project" InitialValue="0" Display="None"
                                        ValidationGroup="Footer_Expenses"></asp:RequiredFieldValidator>   
                                </FooterTemplate>
                                <FooterStyle Width="110" />
                            </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,EntryType%>">
                            <HeaderStyle Width="150px" />
                            <ItemStyle Width="150px" />
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlEntryTandE" SkinID="ddl_125px" runat="server" DataSourceID="SqlDataSourceEntry2TandE"
                                    AutoPostBack="true" DataTextField="ExpensesentryType" DataValueField="EntryTypeID"
                                    OnSelectedIndexChanged="ddlEntryTandE_SelectedIndexChanged">
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblEntryTandE" runat="server" Text='<%# Bind("ExpensesentryType") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlEntryTandE_footer" SkinID="ddl_125px" runat="server" DataSourceID="SqlDataSourceEntry2TandE_footer"
                                    AutoPostBack="true" DataTextField="ExpensesentryType" DataValueField="EntryTypeID"
                                    OnSelectedIndexChanged="ddlEntryTandE_footer_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:LinkButton ID="btn_Footer_ExternalExpenses11" runat="server" SkinID="BtnLinkAdd"
                                    Enabled="<%#CommandField()%>" CommandName="ExpensesType" />
                                <asp:RequiredFieldValidator ID="RequirAVFDTT5" runat="server" ControlToValidate="ddlEntryTandE_footer"
                                    ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectentrytype%>" InitialValue="0"
                                    Display="None" ValidationGroup="Footer_Expenses"></asp:RequiredFieldValidator>
                            </FooterTemplate>
                            <FooterStyle Width="150px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Qty%>">
                            <HeaderStyle HorizontalAlign="Center" Width="60px" />
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                            <ItemTemplate>
                                <asp:Label ID="lblQty" runat="server" Text='<%# Bind("Qty") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtQty_footerexpenses" runat="server" Text='<%# Bind("Qty") %>'
                                    SkinID="txt_75px"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator2e5" runat="server" ControlToValidate="txtQty_footerexpenses"
                                    Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseentervalidqty%>"
                                    Operator="DataTypeCheck" Type="Double" ValidationGroup="Group100"></asp:CompareValidator>
                            </FooterTemplate>
                            <FooterStyle Width="60px" />
                            <EditItemTemplate>
                                <asp:TextBox ID="txtQtyTandE" runat="server" Text='<%# Bind("Qty") %>' Width="60px"
                                    SkinID="txt_75px"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator2e1" runat="server" ControlToValidate="txtQtyTandE"
                                    Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseentervalidqty%>"
                                    Operator="DataTypeCheck" Type="Double" ValidationGroup="Footer_Expenses"></asp:CompareValidator>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,BuyingPrice%>">
                            <HeaderStyle HorizontalAlign="Center" Width="60px" />
                            <ItemStyle HorizontalAlign="Right" Width="60px" />
                            <ItemTemplate>
                                <asp:Label ID="lblUnitPrice" runat="server" Text='<%# Bind("BuyingPrice","{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtUnitPrice_footerexpenses" runat="server" SkinID="Price_75px"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator2A5" runat="server" ControlToValidate="txtUnitPrice_footerexpenses"
                                    Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Plsentervalidprice%>"
                                    Operator="DataTypeCheck" Type="Double" ValidationGroup="Group100"></asp:CompareValidator>
                            </FooterTemplate>
                            <FooterStyle Width="60px" />
                            <EditItemTemplate>
                                <asp:TextBox ID="txtUnitPriceTandE" runat="server" Text='<%# Bind("BuyingPrice") %>'
                                    SkinID="Price_75px"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator2D1" runat="server" ControlToValidate="txtUnitPriceTandE"
                                    Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Plsentervalidprice%>"
                                    Operator="DataTypeCheck" Type="Double" ValidationGroup="Footer_Expenses"></asp:CompareValidator>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,SellingPrice%>">
                            <HeaderStyle HorizontalAlign="Center" Width="60px" />
                            <ItemStyle HorizontalAlign="Right" Width="60px" />
                            <ItemTemplate>
                                <asp:Label ID="lblSelling" runat="server" Text='<%# Bind("SellingPrice","{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtSelling_footerexpenses" runat="server" SkinID="Price_75px"></asp:TextBox>
                                <asp:CompareValidator ID="C2A5AS" runat="server" ControlToValidate="txtSelling_footerexpenses"
                                    Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Plsentervalidprice%>"
                                    Operator="DataTypeCheck" Type="Double" ValidationGroup="Group100"></asp:CompareValidator>
                            </FooterTemplate>
                            <FooterStyle Width="60px" />
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSellingTandE" runat="server" Text='<%# Bind("SellingPrice") %>'
                                    SkinID="Price_75px"></asp:TextBox>
                                <asp:CompareValidator ID="C2D1" runat="server" ControlToValidate="txtSellingTandE"
                                    Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Plsentervalidprice%>"
                                    Operator="DataTypeCheck" Type="Double" ValidationGroup="Footer_Expenses"></asp:CompareValidator>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,TotalBuyingPrice%>">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Right" Width="30px" />
                            <ItemTemplate>
                                <asp:Label ID="lblAmountTandE" runat="server" Text='<%# Bind("Total","{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterStyle Width="30px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,TotalSellingPrice%>">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Right" Width="30px" />
                            <ItemTemplate>
                                <asp:Label ID="lblSellingTotalTandE" runat="server" Text='<%# Bind("SellingTotal") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterStyle Width="30px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Notes%>">
                            <%-- <ItemStyle Width="100px" />--%>
                            <HeaderStyle HorizontalAlign="Center" />
                            <EditItemTemplate>
                                <asp:TextBox ID="txtNotesTandE" runat="server" SkinID="txt_125px" TextMode="MultiLine"
                                    Text='<%# Bind("Notes") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblNotesTandE" runat="server" Text='<%# Bind("Notes") %>' ToolTip='<%#Bind("Notes") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txt_footerNotes" runat="server" SkinID="txt_125px" TextMode="MultiLine"></asp:TextBox>
                            </FooterTemplate>
                            <%-- <FooterStyle Width="100px" />--%>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="15px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:LinkButton ID="ImageButton1a" runat="server" CausesValidation="false" CommandName="Delete"
                                    Enabled="<%#CommandField()%>" SkinID="BtnLinkDelete" CommandArgument='<%# Bind("ID") %>'
                                    OnClientClick="return confirm('Do you want to delete the record?');" />
                            </ItemTemplate>
                           
                            <FooterStyle Width="45px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSourceEntry2TandE" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                    SelectCommand="SELECT ID as EntryTypeID,ExpensesentryType FROM [ExpensesentryType]">
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSourceEntry2TandE_footer" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                    SelectCommand="DEFFINITY_ExpensesType" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
	</div>
</div>
    <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.Total%></label>
           <div class="col-sm-9 form-inline">
                <asp:Label ID="lblTotalCostofExpenses" Width="50px" runat="server" Style="text-align: right"></asp:Label>
                    &nbsp;<asp:Label ID="lbltoalCost_Expensesselling" Width="50px" runat="server" Style="text-align: right"></asp:Label>
            </div>
	</div>
</div>



    <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong>   <%= Resources.DeffinityRes.ExternalExpenses%> </strong>
            <hr class="no-top-margin" />
            </div>
    </div>

<uc5:PTExpense ID="PTExpense1" runat="server" />

    
    <uc8:CreditNote ID="Credit1" runat="server"></uc8:CreditNote>
</body>
</html>

 