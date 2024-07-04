<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="controls_BOMUploadCtrl" Codebehind="BOMUploadCtrl.ascx.cs" %>
<div class="form-group-no-margin">
    <div class="col-md-12 pull-right">
         <div class="pull-right form-inline">
              <asp:LinkButton ID="btnUploadTemplate" runat="server" SkinID="BtnLinkButton" Text="Upload BOM" CausesValidation="false" Font-Bold="true" /> 
            <asp:LinkButton ID="btnDownloadBoMTemplate" runat="server" SkinID="BtnLinkButton" Text ="Download BOM template"
                OnClick="btnDownloadBoMTemplate_Click" CausesValidation="false" Font-Bold="true" />
              <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server"
                TargetControlID="pnlUploadtemplate" ExpandControlID="btnUploadTemplate" CollapseControlID="btnUploadTemplate"
                TextLabelID="Lbl1" ImageControlID="btnUploadTemplate" Collapsed="true" SuppressPostBack="true">
            </ajaxToolkit:CollapsiblePanelExtender>
         </div>
        </div>
</div>
 <asp:Panel ID="pnlUploadtemplate" runat="server" Width="100%"  >
      <div class="form-group">
          <div class="col-md-12">
              <label><strong> Upload BOM </strong></label>
              </div>
          </div>
     <hr class="no-top-margin" />
     <div class="form-group-no-margin">
           <div class="col-md-12">
                <asp:Label ID="lblUploadErrorMsg" runat="server" ForeColor="Red"></asp:Label>
               </div>
         </div>
      <div class="form-group form-inline">
           <div class="col-md-12 form-inline">
                <div class="form-inline">
                    <asp:FileUpload ID="fileUpload2" runat="server" /><asp:Button ID="btnUploadData" CausesValidation="false"
                        runat="server" SkinID="btnUpload" ImageAlign="AbsMiddle" OnClick="btnUploadData_Click" /></div>
                </div>
          </div>
 </asp:Panel>
           


