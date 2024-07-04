<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InvoiceSeedCtrl.ascx.cs" Inherits="DeffinityAppDev.WF.DC.controls.InvoiceSeedCtrl" %>
<%--<div class="form-group row">
        <div class="col-md-12 text-bold">
             <strong><asp:Label ID="lblHeader" runat="server" Text="Invoice Config"></asp:Label></strong>
            <hr class="no-top-margin" />
            </div>
</div>--%>


<asp:UpdateProgress ID="uprogressDistri" runat="server" AssociatedUpdatePanelID="upd5">
    <ProgressTemplate>
        <asp:Label SkinID="Loading" runat="server"></asp:Label>
    </ProgressTemplate>
</asp:UpdateProgress>
      <asp:UpdatePanel ID="upd5" runat="server" UpdateMode="Conditional">
       <ContentTemplate>
           
<div class="form-group row">
    <div class="col-md-12">
        <asp:Label ID="lblErrorMsg" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
     <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
            <asp:Label ID="lblEmailDisList" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtInvoiceSeed"
                ErrorMessage="Please enter "  ValidationGroup="user" SetFocusOnError="true"></asp:RequiredFieldValidator>
                <asp:HiddenField ID="h_rtid" runat="server" /> 
        </div>
    </div>
<div class="form-group row">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> Invoice Start</label>
                                      <div class="col-sm-8"> <asp:TextBox ID="txtInvoiceSeed" runat="server" SkinID="Price_150px" MaxLength="20"></asp:TextBox>
					</div>
				</div>
                </div>
           <div class="form-group row">
             <div class="col-md-12">
                  <label class="col-sm-2 control-label"> </label>
                 <div class="col-sm-8 form-inline">
                     <asp:LinkButton ID="btnaddUser" runat="server" SkinID="btnSubmit" OnClick="btnaddUser_Click" ValidationGroup="user" />
           
                     </div>
                 </div>
               </div>
           

  </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnaddUser" EventName="click" />                                      
                                       
                                    </Triggers>
                                </asp:UpdatePanel>