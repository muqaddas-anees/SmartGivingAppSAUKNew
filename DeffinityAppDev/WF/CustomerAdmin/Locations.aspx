<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="Locations" Title="Locations" Codebehind="Locations.aspx.cs" %>
<%@ Register Src="controls/PortfolioMenuTab.ascx" TagName="PortfolioMenuTab" TagPrefix="uc1" %>
<%@ Register Src="controls/PortfolioDdlCtr.ascx" TagName="PortfolioDdlCtr" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.CustomerAdmin%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
      Site Configuration - <uc2:PortfolioDdlCtr ID="PortfolioDdlCtr1" runat="server" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" runat="Server">
  <%--  <uc1:PortfolioMenuTab ID="PortfolioMenuTab1" runat="server" />--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">

    <script type="text/javascript">
        function toggleVisibility() {


            var control = document.getElementById('<%=txtCityNew.ClientID %>');
            control.style.visibility = "visible";
            //            if (control.style.visibility == "visible" || control == "")
            //             control.style.visibility = "hidden";
            //            else control.style.visibility = "visible";

            //control.style.visibility == "visible";
            var ddlcontrol = document.getElementById('<%=ddlCity.ClientID %>');
            ddlcontrol.style.visibility = "hidden";
            //            if (control.style.visibility == "visible" || control.style.visibility == "")
            //                control.style.visibility = "hidden";
            //            else control.style.visibility = "visible"; 
        }    </script>
    <div class="row">
<div class="col-md-7">

     <asp:UpdatePanel ID="updtPnlLocation" runat="server">
                    <ContentTemplate>
                       
                            <div>
                                <asp:ValidationSummary ID="vldSummary" runat="server" ValidationGroup="AddSite" />
                                <asp:Label ID="lbltest" runat="server" EnableViewState="false" Visible="false"></asp:Label>
                            </div>
                             <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.Country%></label>
                                      <div class="col-sm-9"> <asp:DropDownList ID="ddlCountry" runat="server" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"
                                            AutoPostBack="True" SkinID="ddl_70">
                                        </asp:DropDownList>
					</div>
				</div>
                                 </div>
                                  <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.City%></label>
                                      <div class="col-sm-9 form-inline">
                                           <asp:DropDownList ID="ddlCity" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged"
                                            SkinID="ddl_70">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtCityNew" runat="server" Visible="false" SkinID="txt_70"></asp:TextBox>
                                        <asp:LinkButton ID="lbtnAddCity" Text="<%$ Resources:DeffinityRes, AddCity%>" Font-Bold="true"
                                            OnClick="lbtnAddCity_Click" runat="server" SkinID="BtnLinkAdd"></asp:LinkButton>
                                        <asp:LinkButton ID="ImgbtnDelCity" runat="server" OnClick="ImgbtnDelCity_Click"
                                            SkinID="BtnLinkDelete" OnClientClick="return confirm('Do you want to delete the City?');" />
                                        <asp:Button ID="imgAddNewCity" runat="server" Visible="false" SkinID="btnAdd"
                                            OnClick="imgAddNewCity_Click" />
                                        <asp:Button ID="imgCancelNewCity" runat="server" Visible="false" SkinID="btnCancel"
                                            OnClick="imgCancelNewCity_Click" />
					</div>
				</div>
                                      </div>
                                       <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-3 control-label">  <%= Resources.DeffinityRes.Site%></label>
                                      <div class="col-sm-9 form-inline">
                                           <asp:DropDownList ID="ddlSite" runat="server" SkinID="ddl_70">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtsitename" runat="server" Visible="false" SkinID="txt_80"></asp:TextBox>
                                        <asp:LinkButton ID="lbtnAddSite" runat="server" OnClick="lbtnAddSite_Click" SkinID="BtnLinkAdd">
                                            </asp:LinkButton>
                                        <asp:LinkButton ID="imgbtnDelSite" runat="server" OnClick="imgbtnDelSite_Click"
                                            SkinID="BtnLinkDelete" OnClientClick="return confirm('Do you want to delete the Site?');" />
                                        <asp:RequiredFieldValidator ID="rqreFldVldtr" runat="server" ControlToValidate="txtsitename"
                                            ValidationGroup="AddSite" ErrorMessage="<%$ Resources:DeffinityRes, Pleaseentersitename%>"
                                            Display="None"></asp:RequiredFieldValidator>
					</div>
				</div>
                                           </div>
                             <asp:Panel ID="DivSite" runat="server" Visible="false">
                                  <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.Address%></label>
                                      <div class="col-sm-9"> <asp:TextBox ID="txtaddr1" runat="server" SkinID="txt_80"></asp:TextBox>
					</div>
				</div>
                                                </div>
                                    <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.Postcode%></label>
                                      <div class="col-sm-9"> <asp:TextBox ID="txtpostcode" runat="server" SkinID="txt_80"></asp:TextBox>
					</div>
				</div>
                                                     </div>
                                    <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-3 control-label"></label>
                                      <div class="col-sm-9 form-inline">
                                            <asp:Button ID="ImgbtnInsertSite" runat="server" SkinID="btnSubmit" OnClick="imgbtnInsertSite_Click"
                                                ValidationGroup="AddSite" />
                                            <asp:Button ID="imgbtnCancelSite" runat="server" SkinID="btnCancel" OnClick="imgbtnCancelSite_Click" />
					</div>
				</div>
                                                     </div>
                                  
                                
                                </asp:Panel>
                                         
                                              
                            <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> </label>
                                      <div class="col-sm-9">  <asp:Button runat="server" ID="btnBack" OnClick="btnBack_Click" Visible="False"
                                            SkinID="btnDefault" Text="Back" />  
					</div>
				</div>
                        </div>
                         </ContentTemplate>
                </asp:UpdatePanel>
    </div>
</div>
      <div class="row" >
<div class="col-md-10">
                <asp:UpdatePanel ID="updatGrid" runat="server"><ContentTemplate>
                       
                            <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                <asp:GridView ID="grdSites" runat="server" OnRowCommand="grdSites_RowCommand" OnRowDeleting="grdSites_RowDeleting"
                                    Width="100%" PageSize="5" OnPageIndexChanging="grdSites_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField DataField="ID" Visible="false" />
                                        <asp:BoundField HeaderText="<%$ Resources:DeffinityRes, Country%>" DataField="Country">
                                            <ItemStyle Width="150" />
                                            <HeaderStyle CssClass="header_bg_l" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="<%$ Resources:DeffinityRes, City%>" DataField="City">
                                            <ItemStyle Width="150" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="<%$ Resources:DeffinityRes, Site%>" DataField="Site">
                                            <ItemStyle Width="200" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="<%$ Resources:DeffinityRes, Address%>" DataField="Address">
                                            <ItemStyle Width="300" />
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButtonDelete1" runat="server" CommandName="Delete" CommandArgument="<%# Bind('ID')%>"
                                                    SkinID="BtnLinkDelete" OnClientClick="return confirm('Do you want to delete the Assigned Site?');" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            <HeaderStyle CssClass="header_bg_r" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                       
                        </ContentTemplate></asp:UpdatePanel>
    </div>
    </div>

   
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
 <script type="text/javascript">
     //grid_responsive();
     grid_responsive_display();
     $(window).load(function () {
         $("button:contains('Display all')").click(function (e) {
             e.preventDefault();
             $(".dropdown-menu li")
       .find("input[type='checkbox']")
       .prop('checked', 'checked').trigger('change');
         });
     });
    </script> 
</asp:Content>
