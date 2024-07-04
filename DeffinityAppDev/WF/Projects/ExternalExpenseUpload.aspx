<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainFrame.Master"
     AutoEventWireup="true" Inherits="DeffinityAppDev.WF.Projects.ExternalExpenseUpload" Codebehind="ExternalExpenseUpload.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="form-group">
          <div class="col-md-8">
              <asp:Label ID="lblMsg" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
          </div>
    </div>
    <div class="form-group">
           <div class="col-md-8">
                <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.SelectFile%> </label>
                <div class="col-xs-6">
                    <asp:FileUpload ID="fileUpload" runat="server"  />
                </div>
                 <div class="col-xs-3 form-inline">
                     <asp:Button id="btnUpload" runat="server" SkinID="btnUpload" OnClick="btnUpload_Click" />&nbsp;
                     <asp:Button ID="btnCancel" runat="server" SkinID="btnCancel" />
                 </div>
           </div>
             
    </div>
</asp:Content>

