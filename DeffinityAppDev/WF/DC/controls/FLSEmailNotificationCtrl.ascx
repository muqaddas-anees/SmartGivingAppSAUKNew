<%@ Control Language="C#" AutoEventWireup="true" Inherits="DC_FLSEmailNotificationCtrl" Codebehind="FLSEmailNotificationCtrl.ascx.cs" %>

<%--<div class="form-group row">
        <div class="col-md-12 text-bold">
             <strong>  Email Notification</strong>
            <hr class="no-top-margin" />
            </div>
</div>--%>
<asp:UpdateProgress ID="uprogress1" runat="server" AssociatedUpdatePanelID="upnlemail">
    <ProgressTemplate>
        <asp:Label SkinID="Loading" runat="server" />
    </ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="upnlemail" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
<div class="form-group row mb-6">
    
<asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
    <asp:Label ID="lblError" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
    </div>
<div class="form-group row mb-6">
<div class="col-md-12"><asp:CheckBox ID="chkEmailEngineer" runat="server" Text="Email Engineers on Status Change" />
</div>
</div>
<div class="form-group row mb-6">
<div class="col-md-12"><asp:CheckBox ID="chkEmailCustomer" runat="server" Text="Email Customers on Status Change"  />
</div>
</div>
<div class="form-group row mb-6">
<div class="col-md-12 form-inline"><asp:LinkButton ID="btnSubmit" runat="server" SkinID="btnSubmit" OnClick="btnSubmit_Click"  />
            <asp:LinkButton ID="btnCancel" runat="server" SkinID="btnCancel" />
            <asp:Button  ID="btnCopyToAllCustomer" runat="server" SkinID="btnCopytoAllCustomers" OnClick="btnCopyToAllCustomer_Click" Visible="false"/>
</div>
</div>
                                        </ContentTemplate>
    </asp:UpdatePanel>

