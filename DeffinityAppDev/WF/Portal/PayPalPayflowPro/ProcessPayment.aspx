<%@ Page Title="" Language="C#" MasterPageFile="~/WF/CustomerMainTab.master" AutoEventWireup="true" CodeBehind="ProcessPayment.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.PayPalPayflowPro.ProcessPayment_portal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Process Payment
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
     <asp:Label ID="lblHeader" runat="server" CssClass="header" 
                Text="PayPal Payflow Pro Online Credit Card Transaction"></asp:Label>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
      

    <asp:Panel ID="pnlCCD" runat="server">
    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label">Amount to Charge</label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtAmount" runat="server" SkinID="Price_150px" 
                            Width="150px" ReadOnly="true">00.00</asp:TextBox>
            </div>
	</div>
</div>
    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label">Card Type</label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlCardType" runat="server" SkinID="ddl_200px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="MASTERCARD" Text="MASTERCARD"></asp:ListItem>
                            <asp:ListItem Selected="True" Value="VISA" Text="VISA"></asp:ListItem>
                   <asp:ListItem Value="DISCOVER" Text="DISCOVER"></asp:ListItem>
                    <asp:ListItem Value="AMEX" Text="AMEX"></asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="ddlCardType" Display="Dynamic" CssClass="error-text" ErrorMessage="Required" 
                            ></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label">Card Number</label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtCardNumber" runat="server" CssClass="paymentinfo-text" 
                            SkinID="txt_200px" MaxLength="20"></asp:TextBox>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ControlToValidate="txtCardNumber" Display="Dynamic"  ErrorMessage="Please enter Card Number"></asp:RequiredFieldValidator>
               <p>e.g:4111111111111111</p>
            </div>
	</div>
</div>
    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label"> Name on Card</label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtNameOnCard" runat="server" CssClass="paymentinfo-text" 
                            SkinID="txt_200px" MaxLength="250"></asp:TextBox>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            ControlToValidate="txtNameOnCard" Display="Dynamic" ErrorMessage="Please enter Name on Card"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label">Expiration</label>
           <div class="col-sm-9 form-inline">
                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="paymentinfo-text" SkinID="ddl_125px">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="paymentinfo-text"  SkinID="ddl_125px">
                        </asp:DropDownList>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                            ControlToValidate="ddlMonth" Display="Dynamic"  
                            ErrorMessage="Please select Month"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                            ControlToValidate="ddlYear" Display="Dynamic" 
                            ErrorMessage="Please select Year"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label">Card Security Code</label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtCvv" runat="server" CssClass="paymentinfo-text" 
                            SkinID="txt_75px"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtCvv" Display="Dynamic" 
                            ErrorMessage="Please enter CVV"></asp:RequiredFieldValidator>
               <p>e.g: 123</p>
                         <%-- <p>  A code that is printed (not imprinted) on the back of 
                                a credit card. It consist of 3 or 4 digits. </p>--%>
            </div>
	</div>
</div>
    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label"></label>
           <div class="col-sm-9 form-inline">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                            onclick="btnSubmit_Click" />
                        &nbsp;
                        <asp:Button id="btnCancel" runat="server" SkinID="btnCancel" OnClick="btnCancel_Click" />
            </div>
	</div>
</div>
        </asp:Panel>
    <asp:Panel ID="pnlResult" runat="server" Visible="false">
        <div class="form-group row">
          <div class="col-md-12">
              <div class="label label-success" style="font-size:18px"><asp:Literal ID="lblResult" runat="server"></asp:Literal> </div>
        <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor"></asp:Label>
    <%--<asp:Label ID="" runat="server" SkinID="GreenBackcolor"></asp:Label>--%>
        <asp:Label ID="lblError" runat="server" SkinID="RedBackcolor"></asp:Label>
              </div>
            </div>
        <div class="form-group row">
          <div class="col-md-12 col-md-offset-5">
              <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
	</div>
</div>
        </asp:Panel>
     <asp:HiddenField ID="hadid" runat="server" Value="0" />
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
    <script type="text/javascript">
        hidetabs();
    </script>
</asp:Content>
