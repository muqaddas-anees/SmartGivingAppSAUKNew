<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="ProjectMeetings" Title="Untitled Page" Codebehind="ProjectMeetings.aspx.cs" %>

<%@ Register Src="controls/ProjectRef.ascx" TagName="ProjectRef" TagPrefix="uc2" %>

<%@ Register src="controls/ProjectTabs.ascx" tagname="ProjectTabs" tagprefix="uc1" %>

<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectManagement%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
      Project Updates - <Pref:ProjectRef ID="ProjectRef2" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc1:ProjectTabs ID="ProjectTabs1" runat="server" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="row"><div class="col-md-12"> <div class="pull-right form-inline">
        <asp:Button ID="btnAddMeeting" runat="server" OnClick="btnAddMeeting_Click" Text="New Update" />
        </div>
        </div>
        </div>



  <asp:GridView ID="GridMeetings" runat="server" Width="100%" AllowSorting="True" 
        onsorting="GridMeetings_Sorting">
<Columns>
<asp:HyperLinkField  DataNavigateUrlFields="ProjectReference,ID" DataNavigateUrlFormatString="~/WF/Projects/AddMeeting.aspx?Project={0}&meeting={1}"
             Text="Edit" >
             <ItemStyle HorizontalAlign="Center" />
         </asp:HyperLinkField>
<asp:BoundField DataField="MeetingDate" DataFormatString="{0:d}" HtmlEncode="false" 
        HeaderText="Date Logged" ItemStyle-Width="100" >
<ItemStyle Width="100px"></ItemStyle>
    </asp:BoundField>
<asp:BoundField DataField="MeetingTime" HeaderText="Time"  ItemStyle-Width="100">
<ItemStyle Width="100px"></ItemStyle>
    </asp:BoundField>
<asp:BoundField DataField="Attendees" Visible="false" HeaderText="Attendees" 
        ItemStyle-Width="300">
<ItemStyle Width="300px"></ItemStyle>
    </asp:BoundField>
<asp:BoundField DataField="Location" Visible="false" HeaderText="Location"  
        ItemStyle-Width="200">
<ItemStyle Width="200px"></ItemStyle>
    </asp:BoundField>
<asp:TemplateField HeaderText="Project Update" ItemStyle-Width="200px">
<ItemTemplate>
    <asp:Label ID="lblProjectUpdate" runat="server" Text='<%# GetShortDescription(Eval("GeneralNotes").ToString(),Eval("ProjectReference").ToString(),Eval("ID").ToString())%>'></asp:Label>
</ItemTemplate>

<ItemStyle Width="200px"></ItemStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="Lessons Learnt" ItemStyle-Width="200px">
<ItemTemplate>
    <asp:Label ID="lblLessonsLearnt" runat="server" Text='<%# GetShortDescription(Eval("LessonsLearnt").ToString(),Eval("ProjectReference").ToString(),Eval("ID").ToString())%>'></asp:Label>
</ItemTemplate>

<ItemStyle Width="200px"></ItemStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="Key Achievements Last Week" ItemStyle-Width="200px">
<ItemTemplate>
    <asp:Label ID="lblKeyAchievements" runat="server" Text='<%# GetShortDescription(Eval("KeyAchievements").ToString(),Eval("ProjectReference").ToString(),Eval("ID").ToString())%>'></asp:Label>
</ItemTemplate>

<ItemStyle Width="200px"></ItemStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="Key Tasks This Week" ItemStyle-Width="200px">
<ItemTemplate>
    <asp:Label ID="lblKeyTasks" runat="server" Text='<%# GetShortDescription(Eval("KeyTasks").ToString(),Eval("ProjectReference").ToString(),Eval("ID").ToString())%>'></asp:Label>
</ItemTemplate>

<ItemStyle Width="200px"></ItemStyle>
</asp:TemplateField>
<asp:BoundField DataField="GeneralNotes"  HeaderText="Project Update" 
        Visible="false" ItemStyle-Width="300">
<ItemStyle Width="300px"></ItemStyle>
    </asp:BoundField>
<asp:BoundField DataField="LessonsLearnt"  HeaderText="Lessons Learnt"  
        Visible="false" ItemStyle-Width="200" >
<ItemStyle Width="200px"></ItemStyle>
    </asp:BoundField>
<asp:BoundField DataField="KeyAchievements"  
        HeaderText="Key Achievements Last Week" Visible="false" ItemStyle-Width="300">
<ItemStyle Width="300px"></ItemStyle>
    </asp:BoundField>
<asp:BoundField DataField="KeyTasks"  HeaderText="Key Tasks This Week"  
        Visible="false" ItemStyle-Width="200" >
<ItemStyle Width="200px"></ItemStyle>
    </asp:BoundField>
<asp:TemplateField HeaderText="RAG" ItemStyle-HorizontalAlign="Center"  SortExpression="RAGStatus">
<ItemTemplate>
    <asp:Image ID="Image1" runat="server" Visible='<%#SetVisible(Eval("RAGStatus").ToString())%>' ImageUrl='<%#GetImage(Eval("RAGStatus").ToString())%>'/>
</ItemTemplate>
</asp:TemplateField>
<asp:HyperLinkField  HeaderStyle-CssClass="header_bg_r"  DataNavigateUrlFields="ID" DataNavigateUrlFormatString="~/Reports/ProjectMeeting.aspx?meeting={0}"
             Text="&lt;img src='media/ico_report.png' border='0'/&gt;" Target="_blank" HeaderText="Report" >
             <ItemStyle HorizontalAlign="Center" />
         </asp:HyperLinkField>
</Columns>
<EmptyDataTemplate>
<label>No Updates logged.</label>
</EmptyDataTemplate>
</asp:GridView>

</asp:Content>

