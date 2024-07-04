<%@ Control Language="C#" AutoEventWireup="true" Inherits="DC_controls_EmailFooter" Codebehind="EmailFooter.ascx.cs" %>
<%--<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>--%>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %> 
 <script src="../../Content/assets/js/ckeditor/ckeditor.js"></script>
    <script src="../../Content/assets/js/ckeditor/adapters/jquery.js"></script>
<script type="text/javascript">

    CKEDITOR.config.toolbar = [
	{ name: 'document', groups: ['mode', 'document', 'doctools'], items: ['Source', '-', 'Save', 'NewPage', 'Preview', 'Print', '-', 'Templates'] },
	{ name: 'clipboard', groups: ['clipboard', 'undo'], items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
	{ name: 'editing', groups: ['find', 'selection', 'spellchecker'], items: ['Find', 'Replace', '-', 'SelectAll', '-', 'Scayt'] },
	{ name: 'forms', items: ['Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton', 'HiddenField'] },
	'/',
	{ name: 'basicstyles', groups: ['basicstyles', 'cleanup'], items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'CopyFormatting', 'RemoveFormat'] },
	{ name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi'], items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', 'CreateDiv', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl', 'Language'] },
	{ name: 'links', items: ['Link', 'Unlink', 'Anchor'] },
	{ name: 'insert', items: ['Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak', 'Iframe'] },
	'/',
	{ name: 'styles', items: ['Styles', 'Format', 'Font', 'FontSize'] },
	{ name: 'colors', items: ['TextColor', 'BGColor'] },
	{ name: 'tools', items: ['Maximize', 'ShowBlocks'] },
	{ name: 'others', items: ['-'] },
	{ name: 'about', items: ['About'] }
    ];
</script>
<%--<div class="form-group row">
        <div class="col-md-12 text-bold">
             <strong>  Email Footer </strong>
            <hr class="no-top-margin" />
            </div>
</div>--%>
 <asp:UpdateProgress ID="uprogress1" runat="server" AssociatedUpdatePanelID="upd5">
    <ProgressTemplate>
        <asp:Label SkinID="Loading" runat="server" />
    </ProgressTemplate>
</asp:UpdateProgress>
 <asp:UpdatePanel ID="upd5" runat="server" UpdateMode="Conditional">
       <ContentTemplate>
           <div class="form-group row">
               <div class="col-md-12">
            <asp:Label ID="lblmsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
         <asp:Label ID="lblerror" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
                   </div>
               </div>
<asp:Panel ID="pnlLable" runat="server">
     <div class="form-group row mb-6">
        
         </div>
    <div class="form-group row mb-6">
             
                                       <label class="col-sm-4 control-label"> Select Type of Request</label>
                                      <div class="col-sm-8"><asp:DropDownList ID="ddlRtype" 
    runat="server" onselectedindexchanged="ddlRtype_SelectedIndexChanged" 
    AutoPostBack="True"></asp:DropDownList>
					</div>
				
                </div>

    </asp:Panel>
    
   
    <asp:Panel ID="pnlfooter" runat="server">
         <div class="form-group row mb-6">
           
                  <CKEditor:CKEditorControl ID="editfooter" BasePath="~/Scripts/ckeditor/" runat="server"
                          Width="800px" Height="400px" ClientIDMode="Static"  ></CKEditor:CKEditorControl>
       <%-- <asp:TextBox runat="server"
        ID="editfooter" 
        TextMode="MultiLine" 
        Width="600px" Height="400px"
         />--%>
    
   <%-- <ajaxToolkit:HtmlEditorExtender 
        ID="htmlEditorExtender1" 
        TargetControlID="editfooter" 
        runat="server" DisplaySourceTab="true" >
    </ajaxToolkit:HtmlEditorExtender>--%>
                
             </div>
          <div class="form-group row mb-6">
                <div class="col-md-12">
<asp:Button ID="imgbtnsubmit" runat="server" SkinID="btnSubmit" Text="Submit" onclick="imgbtnsubmit_Click" 
             /> <asp:Button ID="btnCopyToAllCustomer" runat="server" SkinID="btnCopytoAllCustomers" OnClick="btnCopyToAllCustomer_Click" Visible="false" />
              </div>
              </div>
              </asp:Panel>

           </ContentTemplate>
     </asp:UpdatePanel>