<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master"  AutoEventWireup="true" Inherits="QAApprove" Title="QA Approve" Codebehind="QAApprove.aspx.cs" %>

<%@ Register Src="controls/QAtabs.ascx" TagName="QATab1" TagPrefix="QA1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.QA%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.Approval%> - <Pref:ProjectRef ID="ProjectRef1" runat="server" /> 
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
<QA1:QATab1 ID ="QATab1" runat="server" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<script language="javascript" type="text/javascript" src="js/overlib.js"></script>

    
<div class="form-group">
        <div class="col-md-12">
           <strong> <%= Resources.DeffinityRes.CustomerSatisfaction%></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
     
    
<div class="form-group">
          <div class="col-md-12">
              <asp:Panel ID="Panel1" runat="server"  Width="100%" >
                  
<div class="form-group">
          <div class="col-md-12">
              <p>
 Once you are satisfied that the project has been completed, it is recommended that you send the requester a customer satisfaction survey. Please click on the following link to request feedback from the sender.
</p>
	</div>
</div>
                  
<div class="form-group">
          <div class="col-md-12 form-inline">
              <asp:LinkButton ID="lnksendRequest" runat="server" ForeColor="blue" Enabled="true" Text="Send Customer Satisfaction Survey" OnClick="lnksendRequest_Click"></asp:LinkButton>
<asp:HyperLink ID="hlViewFeedback" Text="View Feedback" runat="server" Visible="false"></asp:HyperLink>
	</div>
</div>
                  
<div class="form-group">
          <div class="col-md-12">
              
<asp:Label ID="lblmsg" runat="server" Text="Customer survey has been sent" ForeColor="Green"></asp:Label>
	</div>
</div>

   </asp:Panel>
	</div>
</div>
    

<asp:Panel ID="Panel2" runat="server"  Width="100%" >
  
    <div class="form-group">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.ProjectApproval%></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>

   
<div class="form-group">
          <div class="col-md-12">
              <p>Use the following buttons to approve or reject the project</p>
	</div>
</div>


<div class="form-group">
          <div class="col-md-12">
           <div class="col-sm-4">
               <asp:TextBox ID ="txtReject" runat="server" />
            </div>
              <div class="col-sm-4">
                  <asp:Button ID="btn_Approve" runat="server" SkinID="btnDefault" Text="Approve" OnClick="btn_Approve_Click1" />
<asp:Button ID="btn_reject" runat="server" SkinID="btnDefault" Text="Cancel" OnClick="btn_reject_Click" />
                  </div>
              <div class="col-sm-4">
                  </div>
	</div>
</div>          

</asp:Panel>
</asp:Content>

