<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="CampaignTemplate.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.Campaign.CampaignTemplate" %>
 <%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Campaign
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    
<%--  <script src="../../../Content/assets/js/ckeditor/ckeditor.js"></script>
    <script src="../../../Content/assets/js/ckeditor/adapters/jquery.js"></script>--%>
    <asp:Label ID="lblTemplateName" runat="server"></asp:Label>  
                <asp:LinkButton ID="BtnAddNewCategory" runat="server"
                     SkinID="BtnLinkAdd" CausesValidation="false" OnClick="BtnAddNewCategory_Click"></asp:LinkButton>
                <asp:LinkButton ID="BtnEditCategory" runat="server" SkinID="BtnLinkEdit" ClientIDMode="Static"
                                         OnClick="BtnEditCategory_Click" ValidationGroup="Cat_E"></asp:LinkButton>
                <asp:LinkButton ID="BtnDeleteCategory" runat="server" SkinID="BtnLinkDelete"
                     OnClientClick="if ( !confirm('Do you want to delete this campaign ?')) return false;"
                                         OnClick="BtnDeleteCategory_Click" ValidationGroup="Cat_E"></asp:LinkButton>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
    <asp:HyperLink ID="linkBack" runat="Server" NavigateUrl="~/WF/CustomerAdmin/Campaign/CampaignList.aspx"><i class="fa fa-arrow-left"></i> Return to Email Templates </asp:HyperLink>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    <style>
      .allfieldsli {
          list-style-type: none;
          padding-left: 5px;
          display: block;
          cursor: pointer;
      }
    </style>

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $("#ddlType").change(function () {
                var index = this.selectedIndex;
                var tag = $("#ddlType").val();
                //alert( $("#ddlType").val());
                CKEDITOR.instances['CKEditor1'].insertHtml("{{" + tag + "}} ");

                return false;
            });
            $('#allfields li').click(function () {
                //var CkText = CKEDITOR.instances['CKEditor1'].getData();
                //var CkTextNew = CkText + "[" + $(this).text() + "] ";

                //alert(CkTextNew);
                //insertHtml
                var tag = $(this).text().trim();
                debugger;
                if (tag == "Logo" || tag == "Name" || tag == "Contact" || tag == "Email" || tag == "Customer’s Full Name" || tag == "My Email" || tag == "My Company Name" || tag == "My Mobile Number" || tag == "My URL")
                    //if (tag != "Forms")
                    CKEDITOR.instances['CKEditor1'].insertHtml("[" + tag + "] ");
                // CKEDITOR.instances['CKEditor1'].setData(CkTextNew);
            });

            //$("#btnApply").click(function (e) {
            //    e.preventDefault();
            //    var txt = $("#txtLinkText").val();
            //    var sid = $("#ddlForms").val();
            //    var gLink = "<a href='[URL]/WF/SurveyPro/EmailRedirect.aspx?i=[Instanceid]&s=" + sid + "&e=[Email]'>" + txt + "</a>";
            //    //$("#div_form").show();
            //    //var CkText = CKEDITOR.instances['CKEditor1'].getData();
            //    // var CkTextNew = "- " + gLink + "- ";
            //    debugger;
            //    //alert(CkTextNew);
            //    //CKEDITOR.instances['CKEditor1'].setData(CkTextNew);
            //    CKEDITOR.instances['CKEditor1'].insertHtml(gLink);

            //    return false;
            //});
        });

    </script>
    <div class="form-group row mb-6">
        
            <asp:Label ID="lblMsgNew" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
            <asp:Label ID="lblError" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
           
        </div>
    <div class="form-group row mb-6">
     
             <div class="col-sm-10">
                  <asp:ValidationSummary ID="valsum" ClientIDMode="Static"
                                             runat="server" DisplayMode="List" />
                  <asp:ValidationSummary ID="ValidationSummary2" runat="server"
                                             DisplayMode="List" ForeColor="Red" ValidationGroup="Cat_E" />

                    <asp:Label ID="lblmsg" runat="server" ClientIDMode="Static"></asp:Label>
                   
                   <asp:RequiredFieldValidator ID="reqsubject" runat="server" ErrorMessage="Please enter subject" ClientIDMode="Static"
                        ForeColor="Red" ControlToValidate="txtsubject" Display="None"></asp:RequiredFieldValidator>
                  
             </div>
       
    </div>

    <ajaxToolkit:ModalPopupExtender ID="MdlPopUp" runat="server" BackgroundCssClass="modalBackground"
            TargetControlID="lblInPop" PopupControlID="PanelAdd" CancelControlID="LnkReclaimeCancel"></ajaxToolkit:ModalPopupExtender>
     <asp:Label ID="lblInPop" runat="server"></asp:Label>
    <asp:Panel ID="PanelAdd" runat="server"  Style="display: none;"
                Width="500px" Height="280px"  CssClass=" card shadow-sm" ScrollBars="None" >
          <div class="card-header">
							<h3 class="card-body"><asp:Label ID="LblHeader" runat="server"></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								<asp:LinkButton ID="LnkReclaimeCancel"
                                                 runat="server" SkinID="BtnLinkCancel" ToolTip="Close"></asp:LinkButton>
								
							</div>
						</div>

        <div class="card-body">
        
         
         <div class="form-group row mb-6">
             
                  <div class="col-sm-11">
                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" ClientIDMode="Static"
                                             DisplayMode="List" ValidationGroup="Cat_E1" />
                      <asp:Label ID="LblMsgInPopUp" runat="server" ClientIDMode="Static" EnableViewState="false"></asp:Label>
                      <asp:Label ID="lblpoperror" runat="server" ClientIDMode="Static" EnableViewState="false" SkinID="RedBackcolor"></asp:Label>
                  </div>
             
         </div>
         <div class="form-group row mb-6 d-flex d-inline">
           
                  <div class="col-sm-4">Email Template Name</div>
                  <div class="col-sm-8 form-inline">
                      <asp:TextBox ID="TxtCName" runat="server" SkinID="txt_90"></asp:TextBox> 
                       <asp:RequiredFieldValidator  style="font-size:small" ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ForeColor="Red"
                           ErrorMessage="Please enter Email Template Name" ControlToValidate="TxtCName" ValidationGroup="group1" ></asp:RequiredFieldValidator>
                  </div>
             
         </div>
         <div class="form-group row mb-6">
           
                  <div class="col-sm-4"></div>
                  <div class="col-sm-6">
                      <asp:Button ID="BtnAddCat" ClientIDMode="Static"
                           runat="server" Text="Add" OnClick="BtnAddCat_Click" SkinID="btnSubmit" ValidationGroup="group1" />
                      <asp:Button ID="BtnCancel" runat="server" Text="Cancel" OnClick="BtnCancel_Click" CausesValidation="false" Visible="false" />
                  </div>
             
         </div>
            </div>
    </asp:Panel>



  

    <div class="form-group row mb-6">
        
              <label class="col-sm-1 control-label"> Email Subject</label>
              <div class="col-sm-5">
                  <asp:TextBox ID="txtsubject" runat="server" ClientIDMode="Static"></asp:TextBox>
                  <asp:RequiredFieldValidator  style="font-size:small" ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ForeColor="Red" 
                      ErrorMessage="Please enter Email Subject" ControlToValidate="txtsubject" ValidationGroup="e1" ></asp:RequiredFieldValidator>
               </div>
              <div class="col-sm-2">
                  <asp:Button ID="btnSaveAs" runat="server" SkinID="btnDefault" Text="Save As New Template" OnClick="btnSaveAs_Click" Visible="false" />
                  </div>
         
    </div>
     <div class="row mb-6">
        <div class="col-lg-6">
           

            </div>

         </div>
    <div class="form-group row mb-6">
         <label class="col-sm-1 control-label">Personalise</label>
           <div class="col-sm-5 mb-6">
                <asp:DropDownList ID="ddlType" runat="server" ClientIDMode="Static">
               
            </asp:DropDownList>
               </div>

          
    </div>
     <div class="form-group row mb-6">
         <div class="col-md-12 form-inline">
            
            
               <div class="col-sm-8 dropitems">

                   	<CKEditor:CKEditorControl ID="CKEditor1" BasePath="~/Scripts/ckeditor_4.20.1/" Skin="moono-lisa" runat="server"
                         Height="650px" ClientIDMode="Static" ></CKEditor:CKEditorControl>
                   
                   <%--<CKEditor:CKEditorControl ID="CKEditor1"  BasePath="~/Scripts/ckeditor_4.20.1/" Skin="moono-lisa"  runat="server"
                         Height="650px" ClientIDMode="Static"
                                         filebrowserImageUploadUrl="Upload.ashx"></CKEditor:CKEditorControl>--%>
               </div>
               <div class="col-sm-2 well" style="padding-left:0px;margin-left:0px;display:none;visibility:hidden;">
                  <h4><span>Template Tags</span></h4>
                   <ul id="allfields" style="list-style-type:none;padding-left:0px;margin-left:0px">
                <li id="node1" class="allfieldsli btn btn-white btn-icon btn-icon-standalone btn-sm" style="margin-left:4px"><i class="fa-file-image-o"></i><span>Logo</span></li>
                       <li id="node3" class="allfieldsli btn btn-white btn-icon btn-icon-standalone btn-sm"><i class="fa-sun-o"></i><span>Name</span></li>
                       <li id="node6" class="allfieldsli btn btn-white btn-icon btn-icon-standalone btn-sm"><i class="fa-link"></i><span>Email</span></li>
                       <li id="node8" class="allfieldsli btn btn-white btn-icon btn-icon-standalone btn-sm"><i class="fa-mobile-phone"></i><span>Contact</span></li>
              
                       <li>
                       <div class="form-group row mb-6" id="div_form" style="display:none;padding-left:5px;">
                            <div class="col-md-12">
                        <asp:DropDownList ID="ddlForms" runat="server" ClientIDMode="Static"></asp:DropDownList>
                        <br />
                        <label>Link Title:</label>
                        <asp:TextBox ID="txtLinkText" runat="server" Text="Click here" ClientIDMode="Static"></asp:TextBox>
                                <br />
                                <br />
                        <asp:Button ID="btnApply" runat="server" ClientIDMode="Static" SkinID="btnDefault" Text="Apply" CausesValidation="false" />
                                </div>
                    </div>

                           </li>
            </ul>
              </div>
          </div>
         </div>
    <div class="form-group row mb-6">
          <div class="col-md-12">
              <label class="col-sm-2 control-label">Attachments</label>
               <div class="col-sm-10">
                    <asp:FileUpload ID="fileupload" runat="server" ClientIDMode="Static" AllowMultiple="true" />
               </div>
          </div>
    </div>
    <div class="form-group row mb-6">
          <div class="col-md-12">
              <label class="col-sm-2 control-label"></label>
               <div class="col-sm-6">
                   <asp:GridView ID="gridfiles" runat="server" AutoGenerateColumns="false">
                       <Columns>
                      <%-- <asp:BoundField DataField="Text" HeaderText="File Name" />--%>

                           <asp:TemplateField HeaderText="File Name">
                               <ItemTemplate>
                                   <asp:LinkButton ID = "lnkDownload" OnClick="DownloadFile" CausesValidation="false" 
                                 Text = '<%# Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' runat = "server"></asp:LinkButton>
                               </ItemTemplate>
                           </asp:TemplateField>
                      <asp:TemplateField>
                           <ItemTemplate>
                            <%-- <asp:LinkButton ID = "lnkDelete" OnClick = "DeleteFile" CausesValidation="false" 
                                 Text = "Delete" CommandArgument = '<%# Eval("Value") %>' runat = "server"></asp:LinkButton>--%>
                     <asp:LinkButton runat="server" ID="lnkDelete" CausesValidation="false" SkinID="BtnLinkDelete"
                               CommandArgument = '<%# Eval("Value") %>'
                          OnClientClick="return confirm('Do you want to delete the record?');" OnClick="DeleteFile"></asp:LinkButton>
                           </ItemTemplate>
                      </asp:TemplateField>
                 </Columns>
                </asp:GridView>
               </div>
          </div>
    </div>
    <div class="form-group row mb-6">
      <div class="col-sm-10">
          </div>
             <div class="col-sm-2">
                    <asp:Button ID="btnNext" runat="server" ClientIDMode="Static"
                          OnClick="btnSave_Click" Text="Next" SkinID="btnDefault" ValidationGroup="e1"/>
                   </div>
        
    </div>

     <div class="form-group row mb-6" style="display:none;visibility:hidden;">
          <div class="col-md-12">
              <label class="col-sm-2 control-label"></label>
               <div class="col-sm-8">
                    
                      <asp:Button ID="btnSave" runat="server" ClientIDMode="Static"
                          OnClick="btnSave_Click" Text="Submit" CssClass="btn btn-secondary"/>
                      <asp:Button ID="imgCancel" runat="server" CausesValidation="false" Text="Cancel" OnClick="imgCancel_Click" 
                          CssClass="btn btn-secondary"/>
               </div>

              
          </div>
     </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
    <script>
        hidetabs();
    </script>
</asp:Content>
