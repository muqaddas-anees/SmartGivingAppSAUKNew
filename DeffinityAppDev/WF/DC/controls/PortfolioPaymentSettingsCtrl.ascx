<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortfolioPaymentSettingsCtrl.ascx.cs" Inherits="DeffinityAppDev.WF.DC.controls.PortfolioPaymentSettingsCtrl" %>

<div class="form-group row">
        <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
        <asp:Label ID="lblError" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
    <asp:ValidationSummary ID="payVG" runat="server" ValidationGroup="pay" />
    </div>
<div class="form-group row" >
                                  <div class="row mb-6">
                                      <div class="col-lg-6">
                                      <asp:RadioButtonList ID="ddlpaytype" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="ddlpaytype_SelectedIndexChanged">
                                          <asp:ListItem Text="&lt;img src=&quot; ../../Content/images/icon_stripe.png?d=1 &quot;/&gt;" Value="cardconnect" Selected="True"></asp:ListItem>
                                         <%-- <asp:ListItem Text="&lt;img src=&quot; ../../Content/images/icon_stripe.png?d=1 &quot;/&gt;" Value="stripe"></asp:ListItem>--%>
                                       <%--   <asp:ListItem Text="&lt;img src=&quot; ../../Content/images/icon_merchant.png &quot;/&gt;" Value="mxmerchant"></asp:ListItem>
                                          <asp:ListItem Text="&lt;img src=&quot; ../../Content/images/icon_authorize.png &quot;/&gt;" Value="authorize.net"></asp:ListItem>--%>
                                          
                                      </asp:RadioButtonList>
                                          </div>
                                      </div>
    </div>
<div class="form-group row" >
                                  <div class="row mb-6">
                                      <div class="col-lg-3 gap-3 d-flex d-inline">
                                      <asp:CheckBox ID="chkActive" runat="server" Text="" Checked="true" Visible="false" /> <asp:Label ID="lblset" runat="server" Text="Set Default" Visible="false" ></asp:Label>
                                          </div>
                                      </div>
    </div>
<div class="form-group row" id="pnlVendor" runat="server">
                                  <div class="row mb-6">
                                       <label class="col-sm-2 control-label"><asp:Label ID="lblVendor" runat="server" Text="Publishable key"></asp:Label> </label>
                                      <div class="col-sm-8 form-inline">
                                          <asp:TextBox ID="txtVendor" runat="server" MaxLength="250"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtVendor"
                        Display="None" ErrorMessage="Please enter vendor" ValidationGroup="pay"></asp:RequiredFieldValidator>
                                          </div>
                                      </div>
    </div>
<div class="form-group row" id="pnlPartner" runat="server" style="display:none;">
                                  <div class="row mb-6">
                                       <label class="col-sm-2 control-label"><asp:Label ID="lblPartner" runat="server" Text="Partner"></asp:Label> </label>
                                      <div class="col-sm-8 form-inline">
                                          <asp:TextBox ID="txtPartner" runat="server" MaxLength="250"></asp:TextBox>
                                         <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPartner"
                        Display="None" ErrorMessage="Please enter partner" ValidationGroup="pay"></asp:RequiredFieldValidator>--%>
                                          </div>
                                      </div>
    </div>
<div class="form-group row" id="pnlUsername" runat="server">
                                  <div class="row mb-6">
                                       <label class="col-sm-2 control-label">Username</label>
                                      <div class="col-sm-8 form-inline">
                                          <asp:TextBox ID="txtUsername" runat="server" MaxLength="250"></asp:TextBox>
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtUsername"
                        Display="None" ErrorMessage="Please enter username" ValidationGroup="pay"></asp:RequiredFieldValidator>
                                          </div>
                                      </div>
    </div>
<div class="form-group row" id="pnlPassword" runat="server">
                                  <div class="row mb-6">
                                       <label class="col-sm-2 control-label">Password</label>
                                      <div class="col-sm-8 form-inline">
                                          <asp:TextBox ID="txtPassword" runat="server" MaxLength="250"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfPassword" runat="server" ControlToValidate="txtUsername"
                        Display="None" ErrorMessage="Please enter password" ValidationGroup="pay"></asp:RequiredFieldValidator>
                                          </div>
                                      </div>
    </div>

<div class="form-group row"  id="pnlKey" runat="server">
                                  <div class="row mb-6">
                                       <label class="col-sm-2 control-label"><asp:Literal ID="lblConsumerKey" runat="server" Text="Salt Passphrase"></asp:Literal> </label>
                                      <div class="col-sm-8 form-inline">
                                          <asp:TextBox ID="txtConsumerKey" runat="server" MaxLength="500" Text=""></asp:TextBox>
                                        
                                          </div>
                                      </div>
    </div>
<div class="form-group row"  id="pnlSecret" runat="server">
                                  <div class="row mb-6">
                                       <label class="col-sm-2 control-label"><asp:Literal ID="lblConsumerSecret" runat="server" Text="Consumer Secret"></asp:Literal> </label>
                                      <div class="col-sm-8 form-inline">
                                          <asp:TextBox ID="txtConsumerSecret" runat="server" MaxLength="500" Text=""></asp:TextBox>
                                         
                                          </div>
                                      </div>
    </div>

<div class="form-group row"  id="pnlPriceid_stripe_monthly" runat="server" visible="false">
                                  <div class="row mb-6">
                                       <label class="col-sm-2 control-label">Price ID (Weekly)</label>
                                      <div class="col-sm-8 form-inline">
                                          <asp:TextBox ID="txtPriceIDWeekly" runat="server" MaxLength="500" Text=""></asp:TextBox>
                                        
                                          </div>
                                      </div>
    </div>
<div class="form-group row"  id="pnlPriceID_stripe_yearly" runat="server" visible="false">
                                  <div class="row mb-6">
                                       <label class="col-sm-2 control-label">Price ID (Monthly)</label>
                                      <div class="col-sm-8 form-inline">
                                          <asp:TextBox ID="txtPriceIDMonthly" runat="server" MaxLength="500" Text=""></asp:TextBox>
                                        
                                          </div>
                                      </div>
    </div>

<div class="form-group row"  id="pnlHost" runat="server">
                                  <div class="row mb-6">
                                       <label class="col-sm-2 control-label">Host</label>
                                      <div class="col-sm-8 form-inline">
                                          <asp:TextBox ID="txtHost" runat="server" MaxLength="500" Text="https://sandbox.payfast.co.za/eng/process?"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtHost"
                        Display="None" ErrorMessage="Please enter host" ValidationGroup="pay"></asp:RequiredFieldValidator>
                                          </div>
                                      </div>
    </div>
<div class="form-group row"  id="Div1" runat="server" visible="false">
                                  <div class="row mb-6">
                                       <label class="col-sm-2 control-label">Card Transaction Fee</label>
                                      <div class="col-sm-8 form-inline d-flex d-inline gap-2">
                                          <asp:TextBox ID="txtFee" runat="server" MaxLength="5" Text="0.00" SkinID="Price_125px"></asp:TextBox><span style="font-size:20px;margin-top:10px">%</span>        
                                          
                                        <label class="control-label mt-5">  <asp:Label ID="lblFixedPrice" runat="server" Text="Fixed Price"></asp:Label></label>
                                          <asp:TextBox ID="txtFixedPrice" runat="server" SkinID="Price_125px"></asp:TextBox>
                                          </div>
                                      </div>
    </div>
<div class="form-group row">
                                  <div class="row mb-6 form-inline">
                                      <label class="col-sm-2 control-label"></label>
                                       <div class="col-sm-8 form-inline">
                                      <asp:Button ID="btnSubmitSettings" runat="server" ValidationGroup="pay" SkinID="btnSubmit" OnClick="btnSubmitSettings_Click" style="margin-right:10px"/>
                                           <asp:Button ID="btnValidateMID" runat="server" SkinID="btnDefault" Text="Validate MID" ClientIDMode="Static"  OnClick="btnValidateMID_Click" Visible="false" />
                                           </div>
                                      </div>
    </div>