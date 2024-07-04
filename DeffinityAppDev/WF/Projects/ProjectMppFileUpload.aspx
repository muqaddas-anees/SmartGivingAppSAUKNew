<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainFrame.master" AutoEventWireup="true" Inherits="ProjectMppFileUpload" Codebehind="ProjectMppFileUpload.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="form-group" >
    <div class="col-md-12 text-bold">
                     <h5><strong>MS Project file upload</strong> </h5>
                        <hr class="no-top-margin" />
         <asp:Label ID="lblMsg" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
                    </div>
        </div>
    <div class="form-group form-inline" >
     <div class="col-md-6">
    <label class="col-sm-2 control-label"><%= Resources.DeffinityRes.SelectFile%></label>
         <div class="col-sm-10">
    <asp:FileUpload ID="filempp" runat="server"  />
   </div> 
   </div>
        </div>
 <div class="form-group form-inline" >
     <div class="col-md-6 ">
         <label class="col-sm-2 control-label">&nbsp;</label>
         <div class="col-sm-10">
<asp:Button id="btnUpload" runat="server" SkinID="btnUpload" onclick="btnUpload_Click" /> &nbsp;
<asp:Button ID="btnCancel" runat="server" SkinID="btnCancel" />
             </div>
</div>
     </div>
</asp:Content>

