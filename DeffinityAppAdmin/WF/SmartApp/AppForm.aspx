<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master"
         AutoEventWireup="True" Inherits="App_AppForm" EnableEventValidation="false" Codebehind="AppForm.aspx.cs" %>
<asp:Content ID="Content5" ContentPlaceHolderID="page_title" runat="Server">
     Smart Apps
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="panel_options" runat="Server">
    <asp:HyperLink ID="link_return" runat="Server" NavigateUrl="~/WF/SmartApp/AppFormList.aspx?Appid=0">
<i class="fa fa-arrow-left"></i> Return to App list</asp:HyperLink>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="panel_title" runat="Server">
       <label id="lblTitle" runat="server"></label> 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
<%: System.Web.Optimization.Scripts.Render("~/bundles/jqueryui") %>
<%: System.Web.Optimization.Styles.Render("~/bundles/formscss") %>
<%: System.Web.Optimization.Scripts.Render("~/bundles/forms") %>
    
     <%--<script src="../Scripts/jquery-1.9.0.min.js" type="text/javascript"></script>--%>
     <%--<link rel="stylesheet" href="../stylcss/HCstyle.css"/>--%>
    <script type="text/javascript" src="jQuery.print.js"></script>
   <%-- <link rel="stylesheet" href="../stylcss/ButtonStyle.css"/>--%>
     
    <%--<script type="text/javascript" src="../Scripts/HCform.js"></script>--%>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
 <%-- <script src="https://code.jquery.com/jquery-1.10.2.js" type="text/javascript"></script>--%>
 <%-- <script src="https://code.jquery.com/ui/1.11.4/jquery-ui.js" type="text/javascript"></script>--%>
      <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //$(document.body).find("[id$='lblPageHead']").html('Form');

            $('#lblMsg').fadeOut(2000);
           // $("li[id$='form']").addClass('current1');
        });
    </script>
   
    <script type="text/javascript">
        $(document).ready(function () {
            var CallId = GetParameterValues('callid');
            GetCallId(CallId);
        });
        function GetParameterValues(param) {
            var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < url.length; i++) {
                var urlparam = url[i].split('=');
                if (urlparam[0] == param) {
                    return urlparam[1];
                }
            }
        }
        function GetCallId(CallId) {
            var el = $()
            $.ajax({
                url: "../HC/HCWebService.asmx/GetCallId?CallId" + CallId,
                type: "POST",
                data: "{'CallId': '" + CallId + "'}",
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                async: true,
                success: function (data) {
                    if (data.d != '') {
                        debugger;
                        var obj = jQuery.parseJSON(data.d);
                        GetTextboxId(obj);
                    }
                },
                error: function (msg) { setMsg(Error); }
            })
        }
        function GetTextboxId(panelid) {
            var el = $()
            $.ajax({
                url: "../HC/HCWebService.asmx/GetTextBoxId?pid" + panelid,
                type: "POST",
                data: "{'panelid': '" + panelid + "'}",
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                async: true,
                success: function (data) {
                    if (data.d != '') {
                        debugger;
                        var obj = jQuery.parseJSON(data.d);
                        if (obj != '')
                            debugger;
                        $.each(obj, function () {
                            if ($("#ctl00_ctl00_MainContent_MainContent_" + this.CntlID + "").val() =='')
                            {
                                $("#ctl00_ctl00_MainContent_MainContent_" + this.CntlID + "").datepicker({ dateFormat: 'dd/mm/yy' });
                              }
                        });
                    }
                },
                error: function (msg) { setMsg(Error); }
            })
        }
    </script>

      <div class="form-group">        
            <asp:Label ID="lblMsg" runat="server" EnableViewState="false" ForeColor="Green" ClientIDMode="Static"></asp:Label>
            <asp:HiddenField ID="hPermission" runat="server" />
        </div>
         <div id="pnlForm" runat="server"></div>
    <div class="form-group">
        <div class="col-md-12">

     
        <div id="pnlFormData" runat="server" style="margin-left:5px;margin-right:1px;">
             <asp:ValidationSummary ID="valForm" runat="server" ValidationGroup="Form" />

              <table  width="100%">
                            <tr align="right">
                                <td>
                                    <asp:Button ID="BtnPrint" runat="server" Text="Print" OnClick="BtnPrint_Click" Visible="false"/>
                                </td>
                            </tr>
                </table>

                <asp:UpdatePanel ID="updatepanel_additional" runat="server">
                    <ContentTemplate>
                              <asp:PlaceHolder ID="phMainForm" runat="server"></asp:PlaceHolder>
                        </ContentTemplate>
                </asp:UpdatePanel>
               <div>
                <asp:Button ID="btnSubmitChanges" runat="server" SkinID="btnSubmit"
                                         ValidationGroup="Form" OnClick="btnSubmitChanges_Click" />
              </div>

            <div id="Child1" runat="server" class="form-group">
                <div class="form-group">
                    <div class="col-md-12">
                        <strong>Records </strong> 
                        <hr class="no-top-margin" />
                    </div>
                    <div class="col-md-12">
                          <div style="float:right;text-align:right;">
                              <asp:Button ID="btnPopUp" runat="server" Text="Create child record" OnClick="btnPopUp_Click" />
                          </div>
                    </div>
                </div>
               

                <div class="form-group" id="DivGrid" runat="server">
                        <asp:GridView ID="GridChildrecords" runat="server" AutoGenerateColumns="true" ShowHeader="true" ShowFooter="false" SkinID="Custom_Grid"
                                Width="100%" OnRowCommand="GridChildrecords_RowCommand" OnRowDataBound="GridChildrecords_RowDataBound"
                             OnRowEditing="GridChildrecords_RowEditing" OnRowDeleting="GridChildrecords_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-Width="5%">
                                          <ItemTemplate>
                                              <asp:LinkButton ID="btnEdit" SkinID="BtnLinkEdit" runat="server" Text="Edit"
                                                    CommandName="Edit1" CommandArgument='<%#Bind("ChildID") %>'></asp:LinkButton>
                                                <%--<asp:ImageButton ID="ImgDelete" runat="server" OnClientClick="return confirm('Do you want to delete this record?');"
                                                     Visible="false" CommandArgument='<%#Bind("_id") %>' SkinID="ImgSymDel" ImageAlign="AbsMiddle" CommandName="Delete1" />--%>
                                          </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCidForFile" runat="server" Text='<%#Bind("ChildID") %>' Visible="false"></asp:Label>
                                            <asp:LinkButton SkinID="BtnLinkUpload"
                                                 ID="LnkUploadFile" runat="server" Text="File Upload" CommandName="Upload" CommandArgument='<%#Bind("ChildID") %>' ToolTip="File upload" ></asp:LinkButton> 
                                            <asp:LinkButton SkinID="BtnLinkDownload"
                                                 ID="LnlDownloadFile" runat="server" Text="Download" CommandName="Download" CommandArgument='<%#Bind("ChildID") %>' ToolTip="Download file" ></asp:LinkButton>
                                            <asp:LinkButton SkinID="BtnLinkDelete" ID="LnlDeletefile"
                                                 runat="server" Text="Delete" CommandName="DeleteFile" CommandArgument='<%#Bind("ChildID") %>'
                                                 ToolTip="Delete File" OnClientClick="return confirm('Do you want to delete this file?');" Visible='<%# ApplyPermission() %>' ></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                             <asp:LinkButton ID="btnDelete" SkinID="BtnLinkDelete" runat="server" Text="Delete"
                                                    CommandName="Delete" CommandArgument='<%#Bind("ChildID") %>' OnClientClick="return confirm('Do you want to delete this record?');"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                </div>
                 <ajaxToolkit:ModalPopupExtender ID="mdlpopupForNewItem" runat="server" BackgroundCssClass="modalBackground"
                                  TargetControlID="l11" PopupControlID="PanelForNewChild" CancelControlID="ImageButton4"></ajaxToolkit:ModalPopupExtender>
                <ajaxToolkit:ModalPopupExtender ID="MdlpopForFileUpload" runat="server" BackgroundCssClass="modalBackground" TargetControlID="l11"
                     PopupControlID="PnlFileUpload" CancelControlID="ImageButton2"></ajaxToolkit:ModalPopupExtender>
 
                <asp:Label ID="l11" runat="server"></asp:Label>
                   <asp:Panel ID="PnlFileUpload" runat="server" BackColor="White" Height="150px"
                        Style="display:none;overflow-y:auto;" BorderStyle="Double" BorderColor="LightSteelBlue" ScrollBars="None">
                         
                       <div class="form-group">
                           <div class="col-md-10">
                               <strong> <asp:Label ID="Label1" Text="File upload" runat="server"></asp:Label> </strong> 
                               <hr class="no-top-margin" />
                           </div>
                           <div class="col-md-2" style="float: right">
                                <asp:LinkButton ID="ImageButton2" runat="server" SkinID="BtnLinkCancel" ToolTip="<%$ Resources:DeffinityRes,Close%>" />
                          </div>
                       </div>                  
                          <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                   <ProgressTemplate>
                                     <asp:Label ID="imgloading_ForFileUpload" runat="server" SkinID="Loading"></asp:Label>
                                   </ProgressTemplate>
                         </asp:UpdateProgress>
                         <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                             <ContentTemplate>
                                 
                             </ContentTemplate>
                         </asp:UpdatePanel>
                         <div class="form-group">
                              <asp:Label ID="lblMsgInFileUploadPopup" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
                         </div>
                           <div class="form-group">
                               <div class="col-md-12">
                                   <label class="col-sm-3 control-label">Upload file</label>
                                   <div class="col-sm-6">
                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                   </div>
                                   <div class="col-sm-3">
                                         <asp:Label ID="LblchildFormIdForFile" runat="server" Visible="false"></asp:Label>
                                             <asp:Button ID="BtnFileUpload" runat="server" Text="Upload"
                                                                                           OnClick="BtnFileUpload_Click" Visible='<%# ApplyPermission() %>' />
                                   </div>
                               </div>
                           </div>
                   </asp:Panel>



                   <asp:Panel ID="PanelForNewChild" runat="server" BackColor="White"
                        Style="display:none;overflow-y:auto;" Width="890px" Height="50%" BorderStyle="Double" BorderColor="LightSteelBlue" ScrollBars="None">
                         
                           <div class="form-group">
                               <div class="col-md-10">
                                   <strong><asp:Label ID="Label7" Text="Child form" runat="server"></asp:Label></strong> 
                                    <hr class="no-top-margin" />
                               </div>
                                <div class="col-md-2">
                                      <div style="float: right">
                                          <asp:LinkButton ID="ImageButton4" runat="server" SkinID="BtnLinkCancel" ToolTip="<%$ Resources:DeffinityRes,Close%>" />
                                      </div>
                                </div>
                           </div>                        
                         <asp:UpdateProgress ID="UpdateProgress7" runat="server" AssociatedUpdatePanelID="UpdatePanel_AddItem">
                                   <ProgressTemplate>
                                     <asp:Label ID="imgloading_51" runat="server" SkinID="Loading"></asp:Label>
                                   </ProgressTemplate>
                         </asp:UpdateProgress>
                        <asp:UpdatePanel ID="UpdatePanel_AddItem" runat="server" UpdateMode="Conditional">
                             <ContentTemplate>
                                 <asp:PlaceHolder ID="PhChild" runat="server"></asp:PlaceHolder>

                             </ContentTemplate>
                            </asp:UpdatePanel>
                       <div style="float:left;padding-left:10px;">
                           <asp:Label ID="lblchildFormId" Visible="false" runat="server"></asp:Label>
                           <asp:Label ID="lblupdateorsubmit" runat="server" Visible="false"></asp:Label>
                           <asp:Button ID="imgSaveChildForm" runat="server" SkinID="btnSave"
                                                    ValidationGroup="Form" OnClick="ImageButton1_Click" />
                       </div>
                   </asp:Panel>
            </div>
        </div>
       
               </div>
    </div>
     
     <script type="text/javascript">
         //apply date 
         applyDatePicker();
    </script>
</asp:Content>

