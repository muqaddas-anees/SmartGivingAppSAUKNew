<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="BlogDetails.aspx.cs" Inherits="DeffinityAppDev.WF.Admin.BlogDetails" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %> 
<%@ Register Src="~/WF/Admin/Controls/AdminTabCtrl.ascx" TagPrefix="Pref" TagName="AdminTabCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
  Financial Services
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
     <Pref:AdminTabCtrl runat="server" ID="AdminTabCtrl" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    <asp:Label ID="lblUserfullstuffName" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
    <asp:HyperLink ID="btnBack" runat="server" Text="Back to List" NavigateUrl="~/WF/Admin/BlogList.aspx"></asp:HyperLink>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
     <script src='https://cdn.tiny.cloud/1/no-api-key/tinymce/4/tinymce.min.js'></script>
     <div class="row mb-6">
                                 
                                      <asp:Label ID="lblmsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
                                     
         </div>
     <div class="row mb-6">
                                  
                                       <label class="col-sm-1 control-label">Title</label>
                                      <div class="col-sm-9 form-inline">
                                          <asp:TextBox ID="txtTitle" runat="server" MaxLength="500" ></asp:TextBox>
                                           
                                          </div>
                                     
    </div>
    <div class="row mb-6">
                                 
                                       <label class="col-sm-1 control-label">Is Active</label>
                                      <div class="col-sm-9 form-inline">
                                          <asp:CheckBox ID="chkActive" runat="server" Checked="true" />
                                           
                                          </div>
                                      
    </div>
    <div class="row mb-6">
                                  
                                       <label class="col-sm-1 control-label">Start Date</label>
                                      <div class="col-sm-9 form-inline">
                                       <asp:TextBox ID="txtStartDate" runat="server" SkinID="DateNew"></asp:TextBox>
                                            <asp:Label ID="imgSeheduledDate" runat="server" SkinID="Calender" ClientIDMode="Static" />
                            <ajaxToolkit:CalendarExtender ID="calSeheduledDate" runat="server" CssClass="MyCalendar"
                                PopupButtonID="imgSeheduledDate" TargetControlID="txtStartDate"></ajaxToolkit:CalendarExtender>
                                          </div>
                                    
    </div>
    <div class="row mb-6">
                                 
                                       <label class="col-sm-1 control-label">End Date</label>
                                      <div class="col-sm-9 form-inline">
                                       <asp:TextBox ID="txtEndDate" runat="server" SkinID="DateNew"></asp:TextBox>
                                             <asp:Label ID="lblEndDate" runat="server" SkinID="Calender" ClientIDMode="Static" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
                                PopupButtonID="lblEndDate" TargetControlID="txtEndDate"></ajaxToolkit:CalendarExtender>
                                          </div>
                                     
    </div>
    <div class="row mb-6">
                                 
                                       <label class="col-sm-1 control-label">Image</label>
                                      <div class="col-sm-9 form-inline">
                                         
                                           <asp:FileUpload ID="fileupload" runat="server" />
                                          </div>
                                    
    </div>
    <div class="row mb-6">
                                
        </div>
     <%-- <script src='https://cdn.tiny.cloud/1/no-api-key/tinymce/4/tinymce.min.js'></script>
  <script>
  tinymce.init({
      selector: '#txtHtml'
  });
  </script>--%>

     <%--<textarea id="mytextarea" runat="server">Hello, World!</textarea>
    <asp:TextBox ID="txtHtml" runat="server" ClientIDMode="Static"></asp:TextBox>--%>
      <div class="row mb-6">
                                 
                                       <label class="col-sm-12 control-label"> Content</label>
                                       <div class="col-sm-12 form-inline">
                                            
  <script>
  tinymce.init({
      selector: '#CKEditor1',
      height:500
  });
  </script>

    
    <asp:TextBox ID="CKEditor1" runat="server" ClientIDMode="Static"></asp:TextBox>
                                           </div>

                                     
          </div>
     <div class="row mb-6">
                                  
                                       <label class="col-sm-12 control-label"> </label>
                                      <div class="col-sm-12 form-inline">
                                         <%-- <CKEditor:CKEditorControl ID="CKEditor1" BasePath="~/Scripts/ckeditor/" runat="server"
                         Height="500px" ClientIDMode="Static"></CKEditor:CKEditorControl>--%>
                                           
                                          </div>
                                     
    </div>
     <div class="row mb-6">
                                 
                                       <label class="col-sm-1 control-label">Button1 Title</label>
                                      <div class="col-sm-9 form-inline">
                                         <asp:TextBox ID="txtButtonTitle" runat="server" SkinID="txt_50"></asp:TextBox>
                                           
                                          </div>
                                     
    </div>
         <div class="row mb-6">
                                 
                                       <label class="col-sm-1 control-label">Button1 Link</label>
                                      <div class="col-sm-9 form-inline">
                                         <asp:TextBox ID="txtButtonLink" runat="server" SkinID="txt_80"></asp:TextBox>
                                           
                                        
                                      </div>
    </div>
     <div class="row mb-6">
                                
                                       <label class="col-sm-1 control-label">Button2 Title</label>
                                      <div class="col-sm-9 form-inline">
                                         <asp:TextBox ID="txtButton2Title" runat="server" SkinID="txt_50"></asp:TextBox>
                                           
                                         
                                      </div>
    </div>
         <div class="row mb-6">
                                 
                                       <label class="col-sm-1 control-label">Button1 Link</label>
                                      <div class="col-sm-9 form-inline">
                                         <asp:TextBox ID="txtButton2Link" runat="server" SkinID="txt_80"></asp:TextBox>
                                           
                                         
                                      </div>
    </div>
    <div class="row mb-6">
                                 
                                      
                                      <div class="col-sm-12 form-inline">
                                      <asp:Button ID="btnSave" runat="server" SkinID="btnDefault" Text="Submit" OnClick="btnSave_Click"  />
                                         
                                      </div>
        </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
