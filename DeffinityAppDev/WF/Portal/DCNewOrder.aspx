<%@ Page Title="" Language="C#" MasterPageFile="~/WF/CustomerMainTab.master" AutoEventWireup="true" Inherits="DCNewOrder"  EnableEventValidation ="false" Codebehind="DCNewOrder.aspx.cs" %>
<%@ Register Src="controls/CustomerOrderTabs.ascx" TagName="CustomerOrderTabs" TagPrefix="uc3" %>
<%@ Register src="~/WF/DC/MailControls/CustomerOrderToSDteam.ascx" tagname="CustomerOrderToSDteam" tagprefix="uc1" %>
<%@ Register src="~/WF/DC/MailControls/SDTeamToCustomer_Complete.ascx" tagname="SDTeamToCustomer_Complete" tagprefix="uc2" %>
<%@ Register Src="controls/CART.ascx" TagName="CART" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">

<script type="text/javascript" language="javascript">
    
    function onPopulated() {
        $get('ddlCompany').disabled = true;
        $get('ddlName').disabled = true;
    }
   
    function pageLoad(sender, args) {
        
        $find("ccdC").add_populated(onPopulated);
        $find("ccdrn").add_populated(onPopulated);
       
    }
    $().ready(function () {
        $("#ddlName").change(function () {
            BindValues();
        });
    });
    $().ready(function () {
        BindValues();
    });
    function BindValues() {
        var ID = $('#ddlName').val();
        if (ID != "0") {
            $("#txtReqTelNo").html("");
            $.ajax({
                type: "POST",
                url: "/WF/DC/webservices/DCServices.asmx/GetReqTelNo",
                data: "{ID:'" + ID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('#txtReqTelNo').val(msg.d);
                }
            });

            $("#txtReqEmailAddress").html("");
            $.ajax({
                type: "POST",
                url: "/WF/DC/webservices/DCServices.asmx/GetReqEmail",
                data: "{ID:'" + ID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('#txtReqEmailAddress').val(msg.d); 
                }
            });
        }
        $('#txtReqTelNo').val('');
        $('#txtReqEmailAddress').val('');
    }
    $().ready(function () {
        $("#ddlCompany").change(function () {
            $('#txtReqTelNo').val('');
            $('#txtReqEmailAddress').val('');
        });
    });
</script>
 <uc3:CustomerOrderTabs ID="CustomerOrderTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="Server">
    <%: Resources.DeffinityRes.CustomerPortal %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="panel_title" runat="Server">
   Shopping Cart
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_options" runat="Server">
    <asp:HyperLink ID="linkCustomerRequest" runat="server" 
        Font-Bold="True" NavigateUrl="~/WF/DC/DCCustomerJlist.aspx?type=FLS" Visible="False">Return to Ticket Journal</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
     <div class="form-group row">
          <asp:Label ID="lblMsg" runat="server"  Visible="false"></asp:Label>
         </div>

        
<asp:Panel ID="pnlorder" runat ="server">
        <asp:Panel ID="pnl" runat="server" Width="100%">
             <div class="form-group row">
                   <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="fls"
                DisplayMode="BulletList" />
                 </div>
             <div class="form-group row">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Customer%></label>
                                      <div class="col-sm-8"><asp:DropDownList ID="ddlCompany" runat="server" SkinID="ddl_90" ClientIDMode="Static">
                </asp:DropDownList>
                <ajaxToolkit:CascadingDropDown ID="ccdCompany" runat="server" TargetControlID="ddlCompany" BehaviorID="ccdC"
                    Category="company" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                    ServiceMethod="GetCompany" LoadingText="[Loading customers...]" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCompany"
                    Display="Dynamic" ErrorMessage="Please select customer" InitialValue="0" SetFocusOnError="True"
                    ValidationGroup="fls">*</asp:RequiredFieldValidator>
					</div>
				</div>
 <div class="col-md-6">
                             
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Name%></label>
                                      <div class="col-sm-8"> <asp:DropDownList ID="ddlName" runat="server" SkinID="ddl_90" ClientIDMode="Static">
                </asp:DropDownList>
                <ajaxToolkit:CascadingDropDown ID="ccdName" runat="server" TargetControlID="ddlName"
                    Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                    ServiceMethod="GetNameByCompanyId" ParentControlID="ddlCompany" LoadingText="[Loading name...]" BehaviorID="ccdrn" ClientIDMode="Static" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlName"
                    Display="Dynamic" ErrorMessage="Please select name" InitialValue="0" SetFocusOnError="True"
                    ValidationGroup="fls">*</asp:RequiredFieldValidator>
					</div>        
					</div>
				
</div>
             <div class="form-group row">
                                 
 <div class="col-md-6">
                                        <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.RequesterEmailID%></label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtReqEmailAddress" runat="server" SkinID="txt_90" ClientIDMode="Static"></asp:TextBox>
					</div>
				</div>
                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.ContactNumber%></label>
                                      <div class="col-sm-8">
                                          <asp:TextBox ID="txtReqTelNo" runat="server" SkinID="txt_80" ClientIDMode="Static"></asp:TextBox>
					</div>
				</div>
</div>
            
            <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.Details%></label>
                                      <div class="col-sm-10"> <asp:TextBox ID="txtDetails" runat="server" SkinID="txtMulti" ClientIDMode="Static"></asp:TextBox>
					</div>
				</div>
</div>
<asp:SqlDataSource ID="DS_Priority" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
        SelectCommand="DN_GetIncidentpriority" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
  <asp:ObjectDataSource ID="ObjectDS_Category" runat="server" TypeName="DataHelperClass"  SelectMethod="LoadProjectCategory" OldValuesParameterFormatString="original_{0}">        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="CategoryDropDownFiller" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="CategoryAssociatedToPortfolio_withSelect" TypeName="Deffinity.Bindings.DefaultDatabind" >
                            <SelectParameters>
                            <asp:SessionParameter DefaultValue="0" Name="portfolioid" SessionField="PortfolioID"
                                Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDS_Department" runat="server" TypeName="DataHelperClass"  SelectMethod="LoadPortfolioDepartment" OldValuesParameterFormatString="original_{0}">  </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDS_Site" runat="server" TypeName="Deffinity.Bindings.DefaultDatabind"  SelectMethod="b_SiteSelect_Portfilio_withSelect" OldValuesParameterFormatString="original_{0}">
         <SelectParameters>
                            <asp:SessionParameter DefaultValue="0" Name="portfolioid" SessionField="PortfolioID" Type="Int32" />
                        </SelectParameters>
          </asp:ObjectDataSource>
</asp:Panel>

  <uc1:CustomerOrderToSDteam ID="CustomerOrderToSDteam1" runat="server" Visible="false" Type="FLS" />
        <uc2:SDTeamToCustomer_Complete ID="SDTeamToCustomer_Complete1" runat="server" Visible="false" Type="FLS" />
             <div class="form-group row">
                                  <div class="col-md-12 pull-right">
   <a href="CustomerSC.aspx" style="font-weight:bold;float:right;" >Continue&nbsp;shopping</a>
   </div>
    </div>
    
    <asp:GridView ID="GridView1" runat="server" Width="100%" 
         AutoGenerateColumns="False"  DataSourceID="SqlDataSource1"
        onrowcancelingedit="GridView1_RowCancelingEdit" 
        onrowcommand="GridView1_RowCommand" onrowdatabound="GridView1_RowDataBound" 
        onrowediting="GridView1_RowEditing">
        <Columns>
            
            <asp:TemplateField ItemStyle-CssClass="form-inline" ControlStyle-CssClass="form-inline">
                <FooterStyle Font-Bold="true"  />
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonEdit" runat="server" CommandName="Edit" CommandArgument='<%# Bind("ID")%>'
                        SkinID="BtnLinkEdit" ToolTip="Edit"></asp:LinkButton>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update1" Text="Update"
                        ValidationGroup="Grid" CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkUpdate"
                        ToolTip="Update"></asp:LinkButton>
                    <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                        SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="false">
            <ItemTemplate >
            <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item">
               
                <FooterStyle  Font-Bold="true"  />
                <ItemTemplate>
                    <asp:Label ID="lblitem" runat="server" Text='<%# Bind("Item") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                 <asp:Label ID="lbltotal" runat="server" Text="Total" Font-Bold="true"></asp:Label>  
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Notes">
                <HeaderStyle Width="250px" />
                <ItemStyle Width="250px" />
                <FooterStyle Font-Bold="true"  />
                <EditItemTemplate>
                    <asp:TextBox ID="txtNotes" runat="server" Text='<%# Bind("Notes") %>' TextMode="MultiLine"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblnotes" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Qty">
            <FooterStyle Font-Bold="true"  />
                <%--<EditItemTemplate>
                    <asp:TextBox ID="txtqty" runat="server" Text='<%# Bind("Quantity") %>'></asp:TextBox>
                </EditItemTemplate>--%>
                <ItemTemplate>
                    <asp:TextBox ID="txtqty" runat="server" SkinID="txt_50px" Text='<%# Bind("Quantity") %>'></asp:TextBox>
                    <asp:RegularExpressionValidator Id="regexno" runat="server" ControlToValidate="txtqty" ValidationExpression="^[0-9]*" ErrorMessage="*" ForeColor="Red" ValidationGroup="grpnum"></asp:RegularExpressionValidator>
                </ItemTemplate>
                <FooterTemplate>
                <asp:Label ID="lbltqty" runat="server" ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Unit Price">
             <ItemStyle  HorizontalAlign="Right"/>
            <FooterStyle Font-Bold="true"  />
                <ItemTemplate>
                    <asp:Label ID="lblup" runat="server" Text='<%# Bind("UnitPrice","{0:f2}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total">
            <FooterStyle Font-Bold="true"  HorizontalAlign="Right" />
            <ItemStyle  HorizontalAlign="Right"/>
                <ItemTemplate>
                    <asp:Label ID="lbltotal" runat="server" Text='<%# Bind("Total","{0:f2}") %>'></asp:Label>
                </ItemTemplate>
                 <FooterTemplate>
                 <asp:Label ID="lbltotalSP" runat="server" ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Unit Consumption" Visible="false">
            <ItemStyle  HorizontalAlign="Right"/>
            <FooterStyle Font-Bold="true"   HorizontalAlign="Right" />
                <ItemTemplate>
                    <asp:Label ID="lbluc" runat="server" Text='<%# Bind("UnitConsumption","{0:f2}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total Units" Visible="false">
            <ItemStyle  HorizontalAlign="Right"/>
            <FooterStyle Font-Bold="true"  HorizontalAlign="Right"  />
                <ItemTemplate>
                    <asp:Label ID="lbltotalunits" runat="server" Text='<%# Bind("TotalUnits","{0:f2}") %>'></asp:Label>
                </ItemTemplate>
                 <FooterTemplate>
                  <asp:Label ID="lblftotalUnits" runat="server" ></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
           
            <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                <HeaderStyle Width="70px" />
                <FooterStyle Font-Bold="true"  />
                <ItemTemplate>
                    <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="Delete1"
                        SkinID="BtnLinkDelete" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete the record?');" />
                       
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="70px" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="DEFFINITY_VIEWCART"
    SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="00000000-0000-0000-0000-000000000000" Name="UserID"
            SessionField="cartID" Type="String" />
    </SelectParameters>
</asp:SqlDataSource>
            
            <div class="form-group row">
             <div class="col-md-12 form-inline">
                 <asp:Button ID="btnupdate" runat="server" SkinID="btnUpdate" OnClick="btnupdate_Click"
                    ValidationGroup="grpnum" />
                <asp:Button ID="btnRequestQuote" runat="server" SkinID="btnDefault" Text="Request Quote" 
                    OnClick="btnRequestQuote_Click"  ValidationGroup="fls"/>
                <asp:Button ID="btnProcessOrder" runat="server" SkinID="btnOrange" Text="Process Order"
                    OnClick="btnProcessOrder_Click" ValidationGroup="fls"  />
                  <asp:Button ID="btnClear" runat="server" SkinID="btnClear" OnClick="btnClear_Click" />
</div>
</div>


</asp:Panel>
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
    GridResponsiveCss();
</script> 
</asp:Content>

