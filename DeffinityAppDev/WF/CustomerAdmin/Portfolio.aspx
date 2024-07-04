<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="admin_Portfolio" Title="Portfolio" MaintainScrollPositionOnPostback="true" Codebehind="Portfolio.aspx.cs" %>

<%@ Register src="controls/PortfolioMenuTab.ascx" tagname="PortfolioMenuTab" tagprefix="uc1" %>

<asp:Content ID="ContentTab" ContentPlaceHolderID="Tabs" runat="server">
   <%-- <uc1:PortfolioMenuTab ID="PortfolioMenuTab1" runat="server" />--%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.CustomerAdmin%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
      Add / Edit  <%= Resources.DeffinityRes.Customer%> <b><%=sessionKeys.PortfolioName %></b>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="panel_options" runat="Server">
     <asp:LinkButton runat="server" 
                        ID="lnkPermission" Text="Permission Manager" onclick="lnkPermission_Click"></asp:LinkButton>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts_Section" runat="Server">
    <script type="text/javascript">
        hidetabs();
        seterror('<%= lblError1.Text %>');
    </script>
 </asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <style>
        .modalBackground {
	        background-color:Gray;
	        filter:alpha(opacity=70);
	        opacity:0.7;
        }
</style>

    <asp:Panel ID="Panel1" runat="server" Width="100%">
       <%--  Add  <%= Resources.DeffinityRes.Customer%> --%>
        <div class="form-group row">
            <asp:ValidationSummary ID="Group" runat="server" DisplayMode="List" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                            ValidationGroup="Group"  />
                        <asp:Label ID="lblError" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlPortfolio"
                            ErrorMessage="Please select valid Customer" InitialValue="Please select..."
                            Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>&nbsp;
                        
                        <asp:Label ID="lblError1" runat="server" ForeColor="Red" EnableViewState="false" ></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlOwner"
                            Display="None" ErrorMessage="Please select Account Manager" ValidationGroup="Group" InitialValue="Please select..."></asp:RequiredFieldValidator>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="dlportfoliotype"
                            Display="None" ErrorMessage="Please select Category" ValidationGroup="Group" InitialValue="Please select..."></asp:RequiredFieldValidator>--%>
            </div>

        <div class="form-group row">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.Customer%></label>
                                      <div class="col-sm-6 form-inline">
                                          <%--<div id="divddl" runat="server">--%>
                            <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="txt_field" SkinID="ddl_70"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged"></asp:DropDownList>
                       <%-- </div>--%>
                        <%--<div id="divtxt" runat="server" visible="false">--%>
                            <asp:TextBox ID="txtPortfolio" runat="server" CssClass="txt_field" SkinID="txt_70" Visible="false"></asp:TextBox>
                        <%--</div>--%>
                                          <asp:Button ID="btnNew" runat="server"
                                                                          OnClick="btnNew_Click" CausesValidation="False" SkinID="btnDefault" Text="New" />
                        <asp:Button ID="btnCancel" runat="server" 
                            OnClick="btnCancel_Click" CausesValidation="False" Visible="False" SkinID="btnCancel" />
                                          
					</div>
                  <div class="col-sm-4">
                      </div>
				</div>
                </div>
         <div class="form-group row">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"></label>
                                      <div class="col-sm-6">
                                          <asp:CheckBox ID="chkVisible" Checked="true" runat="server" Text="Customer visible" />
                                          </div>
                 </div>
              <div class="col-sm-4">
                      </div>
             </div>
        <div class="form-group row" runat="server" id="DiVCustomerName" visible="false">
              <div class="col-md-12">
                   <label class="col-sm-2 control-label">Customer Name</label>
                     <div class="col-sm-6">
                         <asp:TextBox ID="TxtCustomerName" runat="server" SkinID="txt_70"></asp:TextBox>
                     </div>
              </div>
        </div>

        <div class="form-group row">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.AccountManager%></label>
                                      <div class="col-sm-6">
                                           <asp:DropDownList ID="ddlOwner" runat="server" SkinID="ddl_70">
                        </asp:DropDownList>
					</div>
                  <div class="col-sm-4">
                      </div>
				</div>
                </div>
         <div class="form-group row">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> Assigned Sales Executive</label>
                                      <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlAssignedSalesExecutive" runat="server" SkinID="ddl_70"></asp:DropDownList>
					</div>
                  <div class="col-sm-4">
                      </div>
				</div>
                </div>
        <div class="form-group row">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> </label>
                                      <div class="col-sm-6"> <asp:CheckBox ID="chkAllowCustomers"  Text="Allow customers to upload documents?"  runat="server"></asp:CheckBox>
					</div>
                  <div class="col-sm-4">
                      </div>
				</div>
                </div>
        <div class="form-group row">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.CustomerLogo%></label>
                                      <div class="col-sm-6 form-inline">
                                         
                                          <asp:FileUpload ID="FileUpload1" runat="server" />
                        
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                            ControlToValidate="FileUpload1" Display="None" 
                            ErrorMessage="Browse an image file  to upload" 
                            ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w ]*.*))+\.(jpg|JPG|gif|GIF|png|PNG)$" 
                            ValidationGroup="Group10">File</asp:RegularExpressionValidator>
                    <br />
                       <asp:Image runat="server" ID="imgLogo"  />
					</div>
                  <div class="col-sm-4">
                      </div>
				</div>
                </div>
        <div class="form-group row">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"></label>
                                      <div class="col-sm-6"> <asp:Button ID="btnSubmit" runat="server" 
                                OnClick="btnSubmit_Click1" 
                                ValidationGroup="Group" SkinID="btnSubmit" />
					</div>
                  <div class="col-sm-4">
                      </div>
				</div>
                </div>
        </asp:Panel>

        <asp:Panel ID="Panel2" runat="server" Width="100%">
            <div class="tab_header_Bold">
                Associated Customers</div>
                <table>
                <tr>
                <td width="100%">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td colspan="2">
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Group5" />
                        <asp:Label ID="Label1" runat="server" ForeColor="Red" Visible="False" EnableViewState="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Customer<span style="color: #ff0000">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCustomer" runat="server" Width="200px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCustomer"
                            Display="None" ErrorMessage="Select a customer" ValidationGroup="Group5" InitialValue="0"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddlCustomer"
                            Display="dynamic" ErrorMessage="*" ValidationGroup="ActGroup" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="height: 30px"></td>
                    <td style="height: 30px">
                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click"
                            ValidationGroup="Group5" SkinID="btnSave" />
                    </td>
                </tr>
            </table>
            </td>
              
                </tr>
                <tr>
                <td>
            <asp:Panel ID="CustomerGridPanel" runat="server" ScrollBars="Auto">
            <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="ID" Visible="false" />
                    <asp:BoundField HeaderText="Customer Name" DataField="ContractorName"  HeaderStyle-CssClass="header_bg_l">
                        <ItemStyle Width="200" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Email Address" DataField="EmailAddress">
                        <ItemStyle Width="200" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Telephone" DataField="Telephone">
                        <ItemStyle Width="200" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderStyle-CssClass="header_bg_r" >
                        <EditItemTemplate>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButtonDelete1" runat="server" CommandName="Delete" Text="Delete"
                                CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkDelete" ToolTip="Delete">
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            </asp:Panel>
            </td>
                </tr>
                </table>
        </asp:Panel>
       <asp:Panel ID="Panel3" runat="server" Width="100%">
            <div class="tab_header_Bold">
                Associated Sites</div>
                 
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td colspan="2">
                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="Groupx" />
                        <asp:Label ID="Label2" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
                        <asp:Label ID="lbltest" runat="server" ForeColor="#FF3300"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 67px">
                        Country<span style="color: #ff0000">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCountry" runat="server" Width="200px" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" 
                            ValidationGroup="Group6">
                        </asp:DropDownList>
                        <asp:LinkButton ID="btnCountry" runat="server" CausesValidation="False" 
                            OnClick="btnCountry_Click" SkinID="BtnLinkAdd" />
                        <asp:RequiredFieldValidator ID="reqcountry" runat="server" ControlToValidate="ddlCountry"
                            Display="None" ErrorMessage="Select a country" InitialValue="0" 
                            ValidationGroup="Groupx"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Panel ID="PnlCountry" runat="server">
                            <asp:TextBox ID="txtCountry" runat="server" Height="16px" ValidationGroup="cntry"
                                Width="197px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqcountry1" runat="server" ControlToValidate="txtCountry"
                                ErrorMessage="Enter Country Name" ValidationGroup="cntry"></asp:RequiredFieldValidator>
                            <br />
                            <asp:Button ID="InsCountry" runat="server"
                                OnClick="InsCountry_Click" ValidationGroup="cntry" SkinID="btnSubmit" />
                            <asp:Button ID="Cancel1" runat="server" CausesValidation="False"
                                OnClick="Cancel1_Click" Style="width: 62px" ValidationGroup="Group3" 
                                SkinID="btnCancel" />
                            <asp:LinkButton ID="btndeleteCountry" runat="server"
                                ValidationGroup="Group3" Visible="False" SkinID="BtnLinkDelete" />
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 67px; height: 8px;">
                        City<span style="color: #ff0000">*</span>
                    </td>
                    <td style="height: 8px">
                        <asp:DropDownList ID="ddlCity" runat="server" Width="200px" AutoPostBack="true" 
                            OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" ValidationGroup="group6">
                        </asp:DropDownList>
                        <asp:LinkButton ID="btnCity" runat="server" CausesValidation="False" 
                            OnClick="btnCity_Click" SkinID="BtnLinkAdd" />
                        <asp:RequiredFieldValidator ID="reqcity" runat="server" ControlToValidate="ddlCity"
                            Display="None" ErrorMessage="Select a city" InitialValue="0" 
                            ValidationGroup="Groupx"></asp:RequiredFieldValidator>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td >
                    </td>
                    <td>
                        <asp:Panel ID="Pnlcity" runat="server">
                            <asp:TextBox ID="txtCity" runat="server" ValidationGroup="city" Width="203px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Enter City Name"
                                ValidationGroup="city" ControlToValidate="txtCity"></asp:RequiredFieldValidator>
                            <br />
                            <asp:Button ID="InsCity" runat="server" OnClick="InsCity_Click"
                                ValidationGroup="city" SkinID="btnSubmit" />
                            &nbsp;<asp:Button ID="Cancel2" runat="server" CausesValidation="False"
                                OnClick="Cancel2_Click" ValidationGroup="Group3" SkinID="btnCancel" />
                            <asp:LinkButton ID="btndeleteCity" runat="server"
                                ValidationGroup="Group3" Visible="False" SkinID="BtnLinkDelete" />
                        </asp:Panel>
                    </td>
                </tr>
                <tr id="pnlhideSite" runat="server">
                    <td style="width: 67px">
                        Site<span style="color: #ff0000">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSite" runat="server" Width="200px" 
                            ValidationGroup="group6">
                        </asp:DropDownList>
                        <asp:LinkButton ID="btnSite" runat="server" CausesValidation="False" 
                            OnClick="btnSite_Click" SkinID="BtnLinkAdd" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlSite"
                            Display="None" ErrorMessage="Select a site" InitialValue="0" 
                            ValidationGroup="Groupx"></asp:RequiredFieldValidator>
                        <br />
                     </td>
                </tr>
                <tr>
                   
                    <td colspan="2" style="width:0px;padding:0px" >
                        <asp:Panel ID="Pnlsite" runat="server" Visible="false" BorderWidth="0">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 95px">
                                        Site<span style="color: #ff0000">*</span></td>
                                    <td>
                                        <asp:TextBox ID="txtSite" runat="server" ValidationGroup="site" Width="195px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                                            ControlToValidate="txtSite" ErrorMessage="Enter Site name" 
                                            ValidationGroup="site"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td >
                                        <label>
                                        Address&nbsp;</label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtaddr1" runat="server" Height="50px" TextMode="MultiLine" 
                                            Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        
                                        <label>
                                       Postcode</label>
                                        
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtpostcode" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:Button ID="InsSite" runat="server" 
                                            OnClick="InsSite_Click" ValidationGroup="site" SkinID="ImgSubmit" />
                                        <asp:Button ID="Cancel3" runat="server" CausesValidation="False" OnClick="Cancel3_Click" 
                                            ValidationGroup="Group3" SkinID="ImgCancel" />
                                        <asp:LinkButton ID="btndeleteSite" runat="server" ValidationGroup="Group3" 
                                            Visible="False" SkinID="BtnLinkDelete" />
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 67px">
                    </td>
                    <td>
                        <asp:Button ID="btnSiteSave" runat="server"
                            ValidationGroup="Groupx" OnClick="btnSiteSave_Click" SkinID="ImgSave" />
                    </td>
                </tr>
            </table>
                   
            <asp:Panel ID="panelGridLocation" runat="server" ScrollBars="Auto">
                <asp:GridView ID="GridView2" runat="server" OnRowCommand="GridView2_RowCommand" 
                    OnRowDeleting="GridView2_RowDeleting" Width="850px">
                    <Columns>
                        <asp:BoundField DataField="ID" Visible="false" />                        
                        <asp:BoundField HeaderText="Country" DataField="Country">
                        <ItemStyle Width="150" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="City" DataField="City">
                        <ItemStyle Width="150" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Site" DataField="Site">
                            <ItemStyle Width="200" />
                        </asp:BoundField>
                         <asp:BoundField HeaderText="Address" DataField="Address">
                            <ItemStyle Width="300" />
                        </asp:BoundField>
                        <asp:TemplateField>
                            <EditItemTemplate>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButtonDelete1" runat="server" CommandName="Delete"
                                                                         CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkDelete"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>
            
        </asp:Panel>
   
    <asp:Button ID="ImageButton1" runat="server"
        OnClick="ImageButton1_Click" CausesValidation="False" Visible="False" 
        SkinID="ImgBack" />
</asp:Content>
