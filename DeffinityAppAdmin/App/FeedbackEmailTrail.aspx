<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="FeedbackEmailTrail.aspx.cs" Inherits="DeffinityAppDev.App.FeedbackEmailTrail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="server">
	Feedback
	</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<div class="card">
<asp:GridView ID="grid_trail" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
    <Columns>
        <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" />
        <asp:BoundField DataField="Subject" HeaderText="Subject" />
        <asp:BoundField DataField="EmailAddress" HeaderText="Email Address" />
        <asp:BoundField DataField="CreatedBy" HeaderText="Sent By" />
        <asp:BoundField DataField="Email" HeaderText="Email Content" />
    </Columns>
</asp:GridView>

	</div>


</asp:Content>