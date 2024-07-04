<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="ThankYouMailSettings.aspx.cs" Inherits="DeffinityAppDev.App.ThankYouMailSettings" %>


<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
Donation Thank You Email
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
   Donation Thank You Email
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
     <a class="btn btn-video" id="btn_video" runat="server" style="background-color:#50CD89;color:white;"  data-class="d-block" data-fslightbox="lightbox-vimeo" href="#vimeo">
   <i class="bi bi-camera-video-fill btn-weight fs-4 me-2 btn-weight"></i> Video Tutorial</a>
                  <iframe id="vimeo" style="display:none" src="https://player.vimeo.com/video/773365031?h=70181f3e39" width="1920px" height="1080px" frameBorder="0" allow="autoplay; fullscreen" allowFullScreen></iframe>

</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
       <style>
           .mycheckBig input {width:18px; height:18px;}
           .mycheckBig label {padding-left:8px}
       </style>
 <%-- <script src="../Content/assets/js/ckeditor/ckeditor.js"></script>
    <script src="../Content/assets/js/ckeditor/adapters/jquery.js"></script>--%>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $("#ddlType").change(function () {
                var index = this.selectedIndex;
                var tag = $("#ddlType").val();
               //alert( $("#ddlType").val());
                CKEDITOR.instances['CKEditor1'].insertHtml("{{" + tag + "}} ");

                return false;
            });

          
        });

    </script>

    <div class="row mb-6" style="display:none;visibility:hidden;">
        <div class="col-lg-12">
            <asp:CheckBox ID="chk" runat="server" Text="Enable Automatic Thank You Emails" CssClass="mycheckBig" />

        </div>

    </div>
     <div class="row mb-6">
        <div class="col-lg-4 d-flex d-inline">
            <asp:Label ID="Label1" runat="server" Text="Template" style="margin-top:10px;padding-right:10px" ></asp:Label>
       
            <asp:DropDownList ID="ddlTemplate" runat="server" ClientIDMode="Static" OnSelectedIndexChanged="ddlTemplate_SelectedIndexChanged" AutoPostBack="true" SkinID="ddl_90" style="padding-right:10px">
               
            </asp:DropDownList>
          
            </div>
        <div class="col-lg-5 d-flex d-inline">
          <asp:Label ID="Label2" runat="server" Text="Send if donation value is between" style="margin-top:10px;padding-right:10px" ></asp:Label>
         <asp:TextBox ID="txtMin" runat="server" SkinID="Price_125px"></asp:TextBox>
          <asp:Label ID="Label3" runat="server" Text="and" style="margin-top:10px;padding-right:10px" ></asp:Label>
          <asp:TextBox ID="txtMax" runat="server" SkinID="Price_125px"></asp:TextBox>
         
         </div>
          <div class="col-lg-3 d-flex d-inline">
          <asp:Button ID="btnAddNew" runat="server" Text="Add" SkinID="btnDefault" OnClick="btnAddNew_Click"  style="margin-left:10px;margin-right:10px"  />
            <asp:Button ID="btnEdit" runat="server" Text="Edit" SkinID="btnDefault" OnClick="btnEdit_Click"   style="margin-left:10px;margin-right:10px"  />
             <asp:LinkButton ID="btnDel" runat="server" Text="Delete" SkinID="BtnLinkDelete" OnClick="btnDel_Click"   style="margin-left:10px;margin-right:10px;padding-top:20px"  />
    </div>
        </div>
    
   
     <div class="row mb-6">
         <style>
             .txt_right{
                 text-align:right;
             }
              .txt_center{
                 text-align:center;
             }
         </style>
         <asp:GridView ID="gridList" runat="server">
             <Columns>
                 <asp:TemplateField  HeaderText="Donation Template">
                     <ItemTemplate>
                         <asp:Label ID="lblTemplate" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                   <asp:TemplateField ItemStyle-CssClass="txt_right" HeaderText="From Amount"  HeaderStyle-CssClass="txt_right" >
                     <ItemTemplate>
                         <asp:Label ID="lblFromAmount" runat="server" Text='<%# Bind("FromAmount","{0:F2}") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                   <asp:TemplateField  ItemStyle-CssClass="txt_right" HeaderText="To Amount"  HeaderStyle-CssClass="txt_right" >
                     <ItemTemplate>
                         <asp:Label ID="lblToAmount" runat="server" Text='<%# Bind("ToAmount","{0:F2}") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
             </Columns>
         </asp:GridView>

         </div>
    <div class="row mb-6">
        <div class="col-lg-6">
            <asp:Label ID="lbl" runat="server" Text="Personalize"></asp:Label>
            <asp:DropDownList ID="ddlType" runat="server" ClientIDMode="Static">
               
            </asp:DropDownList>

            </div>
        <div class="col-lg-6">
            <br />
          
            </div>
        </div>
     <div class="row mb-6" style="display:none;visibility:hidden">
          <div class="col-lg-3 d-flex d-inline">
              <asp:CheckBox ID="chksetdefault" runat="server" Text="Set as default" CssClass="mycheckBig" />
              </div>
          <div class="col-lg-4 d-flex d-inline">
              <asp:CheckBox ID="ChkIsGrater" runat="server" Text="Send if donation greater than" style="margin-right:10px" CssClass="mycheckBig" /> <asp:TextBox ID="txtAmount" runat="server" Text="0.00" SkinID="Price_150px"></asp:TextBox> 
              </div>
           <div class="col-lg-3 d-flex d-inline">
              <asp:CheckBox ID="ChkRecurring" runat="server" Text="Send for recurring donations" CssClass="mycheckBig" />
              </div>
         </div>
     <div class="row mb-6">
        
         </div>
     <div class="row mb-6">
        <div class="col-lg-12">
              <CKEditor:CKEditorControl ID="CKEditor1"  BasePath="~/Scripts/ckeditor_4.20.1/" runat="server" Height="400px"  ClientIDMode="Static" BasicEntities="true" FullPage="true"></CKEditor:CKEditorControl>
            </div>
         </div>
    
     <div class="row mb-6">
        <div class="col-lg-12">
              <asp:Button ID="btnSave" runat="server" Text="Save" SkinID="btnSave" OnClick="btnSave_Click" />
            </div>
         </div>
     <ajaxToolkit:ModalPopupExtender ID="mdlVideo" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="lblclose" PopupControlID="pnlVideo" CancelControlID="lblbtnClose" >
</ajaxToolkit:ModalPopupExtender>
    <asp:Label ID="lblpnl" runat="server"></asp:Label>
    <asp:Label ID="lblclose" runat="server"></asp:Label>
       <asp:Panel ID="pnlVideo" runat="server" BackColor="White"
                       Width="400px" Height="250px" CssClass="Card Card-custom" ScrollBars="None" style="display:none;">
         

             
             <div class="card-header">
							<h3 class="card-title"><asp:Label ID="Label7" runat="server" Text="Add Template"></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="lblbtnClose" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" />
								
							</div>
						</div>
    <div class="card-body">
        <div class="form-group row mb-5">
                  
           <div class="row mb-6">
               <asp:HiddenField ID="hid" runat="server" Value="0" />
               <div class="col-lg-3"> Name</div>
               <div class="col-lg-7"><asp:TextBox ID="txtTemplate" runat="server"></asp:TextBox> </div>
           </div>
                      
                       <div class="row mb-6">
                            <div class="col-lg-12"> <asp:Button id="btnSavetemplate" runat="server" Text="Save" SkinID="btnDefault" OnClick="btnSavetemplate_Click" /> </div>
                           </div>
                       
            </div>
 
      
        
           
        </div>
                  
           </asp:Panel>

</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
