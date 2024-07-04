<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_ForumMaster" Codebehind="ForumMaster.ascx.cs" %>
<%@ Register src="HtmlEditor.ascx" tagname="HtmlEditor" tagprefix="uc1" %>
<%@ Register src="ProjectRef.ascx" tagname="Pref" tagprefix="uc3" %>
<%@ Register src="~/WF/Projects/MailControls/ProjectForumMessageMail.ascx" tagname="ProjectForumMessageMail" tagprefix="uc2" %>
 <div id="MessageDisplayPanle" runat="server">
     <asp:Button ID="btnAddNewpost" runat="server" SkinID="btnDefault" Text="Add New Post" OnClick="btnAddNewpost_Click" /><br /><br />
    <h6 class="altheader">Announcements and FAQs</h6>
    <div runat="server" id="DivNoitems" visible="false" class="blog_tread"> <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr><td>No items exist.</td></tr></table></div>
        <asp:Repeater ID="DataListFourmMaster" runat="server" OnItemCommand="DataListFourmMaster_ItemCommand" OnItemDataBound="DataListFourmMaster_ItemDataBound">
       <HeaderTemplate>
       <table width="100%" border="0" cellspacing="1" cellpadding="0">
        <tr class="tab_header_Bold">
            <td width="4%" scope="col">&nbsp;</td>
            <td colspan="1" scope="col">Thread</td>
            <td width="20%" scope="col">Last Post</td>
            <td width="6%" scope="col">Replies</td>
            <td width="5%" scope="col">Views</td>
          </tr>
       </HeaderTemplate>
       <ItemTemplate>
        <tr class="even_row">
            <td class="blog_post_icon"><%--<img src="media/icon_forum_post.gif" alt="New Post" />--%></td>                   

            <td width="54%">
            <p>
            <asp:HiddenField ID="HidForumMaster" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "id")%>' />
            <asp:LinkButton ID="btnForumItemLink" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Title")%>'></asp:LinkButton></p>
            
            by <span><a href="#" target="_self"><label><%# DataBinder.Eval(Container.DataItem, "AuthorID")%></label></a></span></td>
                    
            <td>
            <p><%# DataBinder.Eval(Container.DataItem, "LatestPost")%>
           </td>
            <td align="center"><label><%# DataBinder.Eval(Container.DataItem, "Replays")%></label></td>
            <td><label><%# DataBinder.Eval(Container.DataItem, "Visited")%></label> </td>
          </tr>
        
       </ItemTemplate>
       <AlternatingItemTemplate>
       
       <tr class="odd_row">
            <td class="blog_post_icon"><%--<img src="media/icon_forum_post.gif" alt="New Post" />--%></td>                   
            <td width="54%">
            <p>
            <asp:HiddenField ID="HidForumMaster" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "id")%>' />
            <asp:LinkButton ID="btnForumItemLink" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Title")%>'></asp:LinkButton></p>
            
            by <span><a href="#" target="_self"><label><%# DataBinder.Eval(Container.DataItem, "AuthorID")%></label></a></span></td>
                    
            <td>
            <p><%# DataBinder.Eval(Container.DataItem, "LatestPost")%>
           </td>
            <td align="center"><label><%# DataBinder.Eval(Container.DataItem, "Replays")%></label></td>
            <td><label><%# DataBinder.Eval(Container.DataItem, "Visited")%></label> </td>
          </tr>
       </AlternatingItemTemplate>
       <FooterTemplate></FooterTemplate>
       </asp:Repeater>
       
       </div>
      
<table Width="100%"><tr><td>
	    <div id="MessagePanle" runat="server" visible="false">
	    <div>
	    <div class="tab_header_Bold">Add/Edit Message</div>
	    <div>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle"
                Display="None" ErrorMessage="Please enter Title" ValidationGroup="GroupVal"></asp:RequiredFieldValidator>
            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="File1"
                Display="None" ErrorMessage="Please enter Message" 
                ValidationGroup="GroupVal"></asp:RequiredFieldValidator>--%>
                <asp:ValidationSummary ID="ValSummary" runat="server" ValidationGroup="GroupVal"/>
                </div>
<label style="font-weight:bold;padding-top:10px;"> Title:</label>
            <asp:HiddenField ID="HiddenField_id" runat="server" />
</div>
<div>
<asp:TextBox ID="txtTitle" runat="server" Width="400px"></asp:TextBox>
</div>
<div>
<label style="font-weight:bold;padding-top:10px">Message:</label>
</div>
<div>

<%--<asp:TextBox ID="txtMessage" Width="600px" Height="100px" TextMode="MultiLine" runat="server"></asp:TextBox>--%>
    <uc1:HtmlEditor ID="txtMessage" runat="server" Width="600" Height="150" />
</div>
<div style="padding-top:10px">
<label style="font-weight:bold;">Attach file: </label>
</div>

<div style="vertical-align:top;padding-top:0px">
<p id="upload-area" style="vertical-align:top;padding-top:0px">
   <input id="File1" type="file" runat="server" size="60" />
</p>
</div>
<div>
<a href="#" onclick="addFileUploadBox()" id="AddFile"><label style="font-weight:bold">Add more files</label></a> 

</div>
<div class="clr"></div>
<div>
<asp:Button runat="server" ID="btnSubmit" SkinID="btnSubmit" OnClick="btnSubmit_Click" ValidationGroup="GroupVal" />&nbsp;<asp:Button runat="server" ID="btnCancel" SkinID="btnCancel" OnClick="btnCancel_Click" CausesValidation="False" />
    <uc2:ProjectForumMessageMail ID="ProjectForumMessageMail1" runat="server" Visible="false" />
</div>
	    
	    </div>
	    
	    </td></tr></table>




	     